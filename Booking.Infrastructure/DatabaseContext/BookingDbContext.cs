using Booking.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.DatabaseContext;

public class BookingDbContext : DbContext
{
    public DbSet<ServiceProvider> ServiceProviders { get; set; } = default!;
    public DbSet<Service> Services { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Appointment> Appointments { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<WorkingHour> WorkingHours { get; set; } = default!;

    public BookingDbContext(DbContextOptions<BookingDbContext> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "BookingDB");
    }
}
