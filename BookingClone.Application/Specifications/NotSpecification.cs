
using System.Linq.Expressions;

namespace BookingClone.Application.Specifications;

public class NotSpecification<T> : Specification<T> where T : class
{
    private readonly ISpecification<T> specification;

    public NotSpecification(ISpecification<T> specification)
    {
        this.specification = specification;
    }
    public override Expression<Func<T, bool>> ToExpression()
    {
        var expr = specification.ToExpression();

        var param = Expression.Parameter(typeof(T));

        var ExpreBody = Expression.Not(Expression.Invoke(expr));

        return Expression.Lambda<Func<T, bool>>(ExpreBody, param);
    }
}
