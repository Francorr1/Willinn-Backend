using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Identity;

namespace Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Data;

public class UserService : IUserService
{
    private readonly UserDbContext _context;
    private readonly PasswordHasher<User> _passwordHasher;

    public UserService(UserDbContext context)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<List<User>> GetAllUsersAsync() => await _context.Users.ToListAsync();
    public async Task<User> GetUserByIdAsync(int id) => await _context.Users.FindAsync(id);
    public async Task AddUserAsync(User user, string password)
    {
        user.Password = _passwordHasher.HashPassword(user, password);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateUserAsync(User user, string password)
    {
        user.Password = _passwordHasher.HashPassword(user, password);
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            user.IsActive = false;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<User> LoginAsync(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    
        if (user == null)
        {
            return null;
        }
        
        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

        if (result == PasswordVerificationResult.Success)
        {
            return user;
        }
        
        return null;
    }
}
