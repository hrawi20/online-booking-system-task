namespace Booking.Core.Entities;

public class Appointment
{
    public int Id { get; set; }
    public DateTime Time { get; set; }
    public string? Note { get; set; }
    public bool IsCancelled { get; set; }
    public bool IsConfirmed { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = default!;

    public int ServiceId { get; set; }
    public Service Service { get; set; } = default!;
}
