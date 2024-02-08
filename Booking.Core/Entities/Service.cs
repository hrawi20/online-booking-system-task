namespace Booking.Core.Entities;

public class Service
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int Duration { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; } = default!;
    public bool IsActive { get; set; }

    public int ProviderId { get; set; }
    public ServiceProvider Provider { get; set; } = default!;

    public int CategoryId { get; set; }
    public Category Category { get; set; } = default!;

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
