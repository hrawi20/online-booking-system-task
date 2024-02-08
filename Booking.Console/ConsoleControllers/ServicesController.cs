using Booking.Core.Entities;
using Booking.Infrastructure.Repositories;

namespace online_booking_system.ConsoleControllers;

public class ServicesController
{
    private readonly IRepository<Service> _serviceRepository;

    public ServicesController(IRepository<Service> serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    public async Task<Service> GetServiceAsync(int id)
    {
        var service = await _serviceRepository.GetAsync(id);
        return service;
    }

    public async Task CreateServiceAsync(Service service)
    {
        await _serviceRepository.AddAsync(service);
    }

    public void UpdateService(Service service)
    {
        _serviceRepository.Update(service);
    }

    public void DeleteService(Service service)
    {
        _serviceRepository.Delete(service);
    }
}
