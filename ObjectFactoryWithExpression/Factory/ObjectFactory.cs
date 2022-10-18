using System.Collections.Concurrent;
using System.Linq.Expressions;
using ObjectFactoryWithExpression.Features.ExpressionCreator;

namespace ObjectFactoryWithExpression.Factory;

public static class ObjectFactory
{
    public static object CreateInstance<T>(this Type type) where T : new() => New<T>.Instance();

    public static object CreateInstance<TArg>(this Type type, TArg arg)
    {
        return CreateInstance<TArg, TypeToIgnore>(type, arg, null);
    }

    public static object CreateInstance<TArg, TArg2>(this Type type, TArg arg, TArg2 arg2)
    {
        return CreateInstance<TArg, TArg2, TypeToIgnore>(type, arg, arg2, null);
    }

    public static object CreateInstance<TArg, TArg2, TArg3>(this Type type, TArg arg, TArg2 arg2, TArg3 arg3)
    {
        return ObjectFactoryCreator<TArg, TArg2, TArg3>.CreateInstance(type, arg, arg2, arg3);
    }

    private static class ObjectFactoryCreator<TArg, TArg2, TArg3>
    {
        private static readonly Type TypeToIgnore = typeof(TypeToIgnore);

        private static readonly ConcurrentDictionary<Type, Func<TArg, TArg2, TArg3, object>>
            _objectFactoryCache = new();

        public static object CreateInstance(Type type, TArg arg, TArg2 arg2, TArg3 arg3)
        {
            var objectFactoryFunc = _objectFactoryCache.GetOrAdd(type, _ =>
            {
                var argumentsTypes = new[]
                {
                    typeof(TArg),
                    typeof(TArg2),
                    typeof(TArg3)
                };

                var constructorArgumentsTypes = argumentsTypes.Where(t => t != TypeToIgnore).ToArray();
                var constructor = type.GetConstructor(constructorArgumentsTypes);
                if (constructor == null)
                    throw new InvalidCastException(
                        $"{type.Name} doesn't contain a constructor for the provided types:" +
                        $" {string.Join(", ", constructorArgumentsTypes.Select(i => i.Name))}");
                var expressionsParameters = argumentsTypes
                    .Select((t, i) => Expression.Parameter(t, $"param{i}"))
                    .ToArray();
                var newExpression = Expression.New(constructor,
                    expressionsParameters.Take(constructorArgumentsTypes.Length).ToArray());
                var expression =
                    Expression.Lambda<Func<TArg, TArg2, TArg3, object>>(newExpression, expressionsParameters);
                return expression.Compile();
            });
            return objectFactoryFunc(arg, arg2, arg3);
        }
    }
}