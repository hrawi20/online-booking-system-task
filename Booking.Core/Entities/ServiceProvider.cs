using Booking.Core.Enums;

namespace Booking.Core.Entities;

public class ServiceProvider
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public ServiceProviderType Type { get; set; }
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Website { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool IsActive { get; set; }
    public bool IsVerified { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = default!;

    public ICollection<Service> Services { get; set; } = new List<Service>();
    public ICollection<WorkingHour> WorkingHours { get; set; } = new List<WorkingHour>();
}
