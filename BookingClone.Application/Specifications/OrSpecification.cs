
using System.Linq.Expressions;

namespace BookingClone.Application.Specifications;

public class OrSpecification<T> : Specification<T> where T : class
{
    private readonly ISpecification<T> left;
    private readonly ISpecification<T> right;

    public OrSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        this.left = left;
        this.right = right;
    }
    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftEpr = left.ToExpression();
        var rightEpr = right.ToExpression();

        var param = Expression.Parameter(typeof(T));

        var fullExpr = Expression
            .OrElse(Expression.Invoke(leftEpr, param), Expression.Invoke(rightEpr, param));

        return Expression.Lambda<Func<T, bool>>(fullExpr, param);
    }
}
