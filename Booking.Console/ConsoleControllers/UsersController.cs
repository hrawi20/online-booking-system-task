using Booking.Core.Entities;
using Booking.Infrastructure.Repositories;

namespace online_booking_system.ConsoleControllers;

public class UsersController
{
    private readonly IRepository<User> _userRepository;

    public UsersController(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserAsync(int id)
    {
        var user = await _userRepository.GetAsync(id, u => u.Appointments, u => u.ServiceProviders);
        return user;
    }

    public async Task CreateUserAsync(User user)
    {
        var existingUsers = await _userRepository.GetAllAsync();
        if (existingUsers.Any(u => u.Email == user.Email || u.PhoneNumber == user.PhoneNumber))
        {
            Console.WriteLine(
                "User with this email or phone number already exists. Please update the existing user."
            );
        }
        else
        {
            await _userRepository.AddAsync(user);
        }
    }

    public async Task UpdateUser(User user)
    {
        _userRepository.Update(user);
    }

    public async Task DeleteUser(User user)
    {
        _userRepository.Delete(user);
    }
}
