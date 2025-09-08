
using System.Linq.Expressions;

namespace BookingClone.Application.Specifications;

public abstract class Specification<T>  : ISpecification<T> where T : class
{
    public abstract Expression<Func<T, bool>> ToExpression();
    public bool IsSatisfiedBy(T entity)
    {
        Func<T, bool> predicate = ToExpression().Compile();
        return predicate(entity);
    }

    public Specification<T> And(ISpecification<T> specification) 
    {
        return new AndSpecification<T>(this, specification);
    }

    public Specification<T> Or(ISpecification<T> specification)
    {
        return new OrSpecification<T>(this, specification);
    }

    public Specification<T> Not()
    {
        return new NotSpecification<T>(this);
    }

}
