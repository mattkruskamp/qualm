# Qualm

A C# library for implementing common microservice patterns. It's focus is first-class support for .Net Standard patterns in order to make the implementations simple and maintainable. The library supports things like:

* Command-Query Responsibility Segregation (CQRS)
* Dependency Injection through IServiceCollection / Provider
* Logging through ILogger<T>

* First-class support for .Net Core standard features like logging in order to make implementations simple and maintainable
* Async is the supported way to implement features

## Getting Started

```
git clone https://github.com/Cyberkruz/qualm.git
```

Open src/Qualm.sln in Visual Studio. Build it.

### Implementing Commands

Create a command:

```csharp
public class TestCommand : ICommand
{
    public TestCommand()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }

    public string Parameter { get; set; }
}
```

Now create the command handler for it.

```csharp
public class TestCommandHandler : CommandHandler<TestCommand>
{
    public override async Task ExecuteAsync(TestCommand command, 
        CancellationToken cancellationToken = default(CancellationToken))
    {
        await Task.Run(() => command.CommandNumber = 20);
    }
}
```

### Implementing Queueing

