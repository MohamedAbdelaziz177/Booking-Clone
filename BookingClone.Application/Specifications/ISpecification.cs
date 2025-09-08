

using System.Linq.Expressions;

namespace BookingClone.Application.Specifications;

public interface ISpecification<T> where T : class
{
    Expression<Func<T, bool>> ToExpression();
    bool IsSatisfiedBy(T entity);
}
