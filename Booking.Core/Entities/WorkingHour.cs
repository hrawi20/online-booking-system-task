namespace Booking.Core.Entities;

public class WorkingHour
{
    public int Id { get; set; }
    public DayOfWeek Day { get; set; }
    public TimeSpan Start { get; set; }
    public TimeSpan End { get; set; }

    public int ProviderId { get; set; }
    public ServiceProvider Provider { get; set; } = default!;
}
