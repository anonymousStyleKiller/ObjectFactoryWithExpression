using System.Linq.Expressions;

namespace ObjectFactoryWithExpression.Features.ExpressionCreator;

public static class New<T>
{
    public static Func<T> Instance = Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();
}