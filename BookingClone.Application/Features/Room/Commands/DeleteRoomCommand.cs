
using BookingClone.Application.Common;
using BookingClone.Application.Features.Room.Responses;
using MediatR;

namespace BookingClone.Application.Features.Room.Commands;

public class DeleteRoomCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
}
