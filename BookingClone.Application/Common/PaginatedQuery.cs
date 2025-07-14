
using MediatR;

namespace BookingClone.Application.Common;

public abstract class PaginatedQuery<T> : IRequest<T> 
{
    public int PageIdx { get; set; } = 1;
    public int PageSize { get; set; } = MagicValues.PAGE_SIZE;

    public string SortField { get; set; } = "Id";

    public string SortType { get; set; } = "DESC";
}
