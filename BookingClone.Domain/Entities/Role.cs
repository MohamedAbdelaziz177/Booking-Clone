
namespace BookingClone.Domain.Entities;

public class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = "USER";

    public ICollection<User> users { get; set; } = default!;
}
