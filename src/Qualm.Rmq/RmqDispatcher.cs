using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Qualm.Commands;
using Qualm.Queuing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qualm.Rmq
{
    public class RmqDispatcher
    {
        private readonly ILogger<RmqDispatcher> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, Type> _commands;
        private readonly RmqChannelFactory _channelFactory;
        private readonly RmqConnectionDetails _connectionDetails;
        private readonly IQueueMessageMapperRegistry _registry;
        private readonly IQueueMessageMapperFactory _factory;
        private readonly Dictionary<string, IModel> _commandChannels;

        public RmqDispatcher(
            IServiceProvider serviceProvider,
            RmqConnectionDetails connectionDetails,
            RmqChannelFactory channelFactory,
            IQueueMessageMapperRegistry registry,
            IQueueMessageMapperFactory factory,
            ILogger<RmqDispatcher> logger)
        {
            _serviceProvider = serviceProvider;
            _commands = new Dictionary<string, Type>();
            _channelFactory = channelFactory;
            _connectionDetails = connectionDetails;
            _registry = registry;
            _factory = factory;
            _commandChannels = new Dictionary<string, IModel>();
            _logger = logger;
        }

        public void AddCommands(Dictionary<string, Type> commands)
        {
            foreach (var command in commands)
            {
                _commands.Add(command.Key, command.Value);
            }
        }

        public void Initialize()
        {
            foreach (var command in _commands)
            {
                var channel = _channelFactory.Create(command.Key);

                channel.QueueBind(
                    command.Key, _connectionDetails.Exchange, command.Key, null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += OnConsumerRecieved;

                channel.BasicQos(0, 1, false);
                channel.BasicConsume(queue: command.Key,
                                 autoAck: false,
                                 consumer: consumer);

                _commandChannels.Add(command.Key, channel);
            }
        }

        protected virtual void OnConsumerRecieved(object sender, BasicDeliverEventArgs e)
        {
         
            var message = new QueueMessage
            {
                Subject = e.RoutingKey,
                Body = Encoding.UTF8.GetString(e.Body.ToArray())
            };

            var type = _commands[message.Subject];
            var channel = _commandChannels[e.RoutingKey];

            var mapperType = _registry.GetMapper(type);
            var mapper = _factory.Create(mapperType);

            var function = new Func<QueueMessage, IRequest>(
                (t) => ((dynamic)mapper).ToRequest((dynamic)message));

            var command = (ICommand)function.Invoke(message);

            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var commandProcessor = services.GetRequiredService<ICommandProcessor>();

                    _logger.LogTrace($"{nameof(commandProcessor): cmd.Id}");

                    commandProcessor.ExecuteAsync(command).GetAwaiter().GetResult();

                    _logger.LogTrace($"{nameof(commandProcessor): cmd.Id finished.}");
                }

                channel.BasicAck(e.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                OnConsumerFailedEventArgs args = new OnConsumerFailedEventArgs();
                args.DeliveryTag = e.DeliveryTag;
                args.Channel = channel;
                args.Command = command;
                args.Error = ex;

                OnConsumerFailed?.Invoke(this, args);
            }
        }

        public event EventHandler<OnConsumerFailedEventArgs> OnConsumerFailed;
    }
}
