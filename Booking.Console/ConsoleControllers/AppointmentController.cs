using System.Linq.Expressions;
using Booking.Core.Entities;
using Booking.Infrastructure.Repositories;

namespace online_booking_system.ConsoleControllers;

public class AppointmentController
{
    private readonly IRepository<Appointment> _appointmentRepository;
    private readonly IRepository<WorkingHour> _workingHourRepository;

    public AppointmentController(
        IRepository<Appointment> appointmentRepository,
        IRepository<WorkingHour> workingHourRepository
    )
    {
        _appointmentRepository = appointmentRepository;
        _workingHourRepository = workingHourRepository;
    }

    public async Task<IEnumerable<WorkingHour>> GetAvailableTimeSlots(int providerId)
    {
        var workingHours = await _workingHourRepository.GetAllAsync(
            new List<Expression<Func<WorkingHour, bool>>> { wh => wh.ProviderId == providerId }
        );
        return workingHours;
    }

    public async Task BookAppointment(Appointment appointment)
    {
        var existingAppointments = await _appointmentRepository.GetAllAsync();
        if (
            existingAppointments.Any(
                a => a.ServiceId == appointment.ServiceId && a.Time == appointment.Time
            )
        )
        {
            Console.WriteLine(
                "There's already a reservation for this service at the requested time and date."
            );
        }
        else
        {
            var providerWorkingHours = await _workingHourRepository.GetAllAsync(
                new List<Expression<Func<WorkingHour, bool>>>
                {
                    wh => wh.ProviderId == appointment.Service.ProviderId
                }
            );

            if (providerWorkingHours.Any(wh => IsWithinWorkingHours(wh, appointment.Time)))
            {
                await _appointmentRepository.AddAsync(appointment);
            }
            else
            {
                Console.WriteLine(
                    "The requested appointment time is outside of the service provider's working hours."
                );
            }
        }
    }

    public async Task ConfirmAppointment(int appointmentId)
    {
        var appointment = await _appointmentRepository.GetAsync(appointmentId);
        if (appointment != null)
        {
            appointment.IsCancelled = true;
            _appointmentRepository.Update(appointment);
        }
        else
        {
            Console.WriteLine("Appointment not found");
        }
    }

    public async Task CancelAppointment(int appointmentId)
    {
        var appointment = await _appointmentRepository.GetAsync(appointmentId);
        if (appointment != null)
        {
            appointment.IsCancelled = true;
            _appointmentRepository.Update(appointment);
        }
        else
        {
            Console.WriteLine("Appointment not found");
        }
    }

    public async Task RescheduleAppointment(int appointmentId, DateTime newTime)
    {
        var appointment = await _appointmentRepository.GetAsync(appointmentId);
        if (appointment != null)
        {
            var providerWorkingHours = await _workingHourRepository.GetAllAsync(
                new List<Expression<Func<WorkingHour, bool>>>
                {
                    wh => wh.ProviderId == appointment.Service.ProviderId
                }
            );

            if (providerWorkingHours.Any(wh => IsWithinWorkingHours(wh, newTime)))
            {
                appointment.Time = newTime;
                _appointmentRepository.Update(appointment);
            }
            else
            {
                Console.WriteLine(
                    "The requested appointment time is outside of the service provider's working hours."
                );
            }
        }
        else
        {
            Console.WriteLine("Appointment not found");
        }
    }

    private static bool IsWithinWorkingHours(WorkingHour workingHour, DateTime appointmentTime)
    {
        var start = new DateTime(
            appointmentTime.Year,
            appointmentTime.Month,
            appointmentTime.Day,
            workingHour.Start.Hours,
            workingHour.Start.Minutes,
            workingHour.Start.Seconds
        );
        var end = new DateTime(
            appointmentTime.Year,
            appointmentTime.Month,
            appointmentTime.Day,
            workingHour.End.Hours,
            workingHour.End.Minutes,
            workingHour.End.Seconds
        );

        return appointmentTime >= start && appointmentTime <= end;
    }
}
