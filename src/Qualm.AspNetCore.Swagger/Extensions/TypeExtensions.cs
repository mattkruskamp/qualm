using System;

namespace Qualm.AspNetCore.Swagger.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsEnum(this Type t, out string enumName)
        {
            if (t.IsEnum)
            {
                enumName = t.Name;
                return true;
            }

            Type u = Nullable.GetUnderlyingType(t);
            enumName = u?.Name;
            return (u != null) && u.IsEnum;
        }
    }
}
