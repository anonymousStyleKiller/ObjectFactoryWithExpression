using ObjectFactoryWithExpression.Features.ExpressionCreator;

namespace ObjectFactoryWithExpression.Factory;

public static class ObjectFactory
{
    public static object CreateInstance<T> (this Type type)where T: new() => New<T>.Instance();
}