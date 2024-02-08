namespace Booking.Core.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool IsActive { get; set; }
    public bool IsVerified { get; set; }

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<ServiceProvider> ServiceProviders { get; set; } =
        new List<ServiceProvider>();
}
