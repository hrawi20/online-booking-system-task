using System.Linq.Expressions;
using Booking.Core.Entities;
using Booking.Infrastructure.Repositories;

namespace online_booking_system.ConsoleControllers;

public class SearchController
{
    private readonly IRepository<Service> _serviceRepository;

    public SearchController(IRepository<Service> serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    public async Task<IEnumerable<Service>> SearchServicesByProviderAsync(int providerId)
    {
        var services = await _serviceRepository.GetAllAsync(
            new List<Expression<Func<Service, bool>>> { s => s.ProviderId == providerId }
        );
        return services;
    }

    public async Task<IEnumerable<Service>> SearchServicesByCategoryAsync(int categoryId)
    {
        var services = await _serviceRepository.GetAllAsync(
            new List<Expression<Func<Service, bool>>> { s => s.CategoryId == categoryId }
        );
        return services;
    }

    public async Task<IEnumerable<Service>> SearchServicesByProviderAndCategoryAsync(
        int providerId,
        int categoryId
    )
    {
        var services = await _serviceRepository.GetAllAsync(
            new List<Expression<Func<Service, bool>>>
            {
                s => s.ProviderId == providerId && s.CategoryId == categoryId
            }
        );
        return services;
    }
}
