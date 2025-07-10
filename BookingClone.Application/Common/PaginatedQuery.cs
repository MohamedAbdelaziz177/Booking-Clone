
using MediatR;

namespace BookingClone.Application.Common;

public abstract class PaginatedQuery<T> : IRequest<T> 
{
    public int PageIdx { get; set; } = 1;
    public int PageSize { get; set; } = 8;

    public string SortField { get; set; } = "Id";

    public SortType SortType { get; set; } = SortType.DESC;
}
