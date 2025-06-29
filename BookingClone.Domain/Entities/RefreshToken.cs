using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Domain.Entities;


public class RefreshToken
{
    
    public int Id { get; set; }

    public string UserId { get; set; } = default!;
    public User User { get; set; } = default!;
    public string Token { get; set; } = default!;
    public bool IsRevoked { get; set; }
    
    public DateTime ExpiryDate { get; set; }
}
