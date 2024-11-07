namespace Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

public interface IUserService
{
    Task<List<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task AddUserAsync(User user, string password);
    Task UpdateUserAsync(User user, string password);
    Task DeleteUserAsync(int id);
    Task<User> LoginAsync(string email, string password);
}