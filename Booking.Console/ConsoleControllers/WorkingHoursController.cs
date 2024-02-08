using Booking.Core.Entities;
using Booking.Infrastructure.Repositories;

namespace online_booking_system.ConsoleControllers;

public class WorkingHoursController
{
    private readonly IRepository<WorkingHour> _workingHourRepository;

    public WorkingHoursController(IRepository<WorkingHour> workingHourRepository)
    {
        _workingHourRepository = workingHourRepository;
    }

    public async Task<WorkingHour> GetWorkingHourAsync(int id)
    {
        var workingHour = await _workingHourRepository.GetAsync(id);
        return workingHour;
    }

    public async Task CreateWorkingHourAsync(WorkingHour workingHour)
    {
        var existingWorkingHour = await _workingHourRepository.GetAllAsync();
        if (
            existingWorkingHour.Any(
                wh => wh.Day == workingHour.Day && wh.ProviderId == workingHour.ProviderId
            )
        )
        {
            Console.WriteLine(
                "Working hour for this day already exists. Please update the existing working hour."
            );
        }
        else
        {
            await _workingHourRepository.AddAsync(workingHour);
        }
    }

    public async Task UpdateWorkingHour(WorkingHour workingHour)
    {
        _workingHourRepository.Update(workingHour);
    }

    public async Task DeleteWorkingHour(WorkingHour workingHour)
    {
        _workingHourRepository.Delete(workingHour);
    }
}
