

using System.Linq.Expressions;

namespace BookingClone.Application.Specifications;

public class AndSpecification<T> : Specification<T> where T : class
{
    private readonly ISpecification<T> left;
    private readonly ISpecification<T> right;

    public AndSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        this.left = left;
        this.right = right;
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpr = left.ToExpression();
        var rightExpr = right.ToExpression();

        var param = Expression.Parameter(typeof(T));

        var fullExpr = Expression
            .AndAlso(Expression.Invoke(leftExpr, param), Expression.Invoke(rightExpr, param));

        return Expression.Lambda<Func<T, bool>>(fullExpr, param);
    }
}
