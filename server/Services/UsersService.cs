using Microsoft.EntityFrameworkCore;
using server.Models;
using server.Providers;

namespace server.Services;

public interface IUserService
{
    Task<List<User>> GetUsers();
    Task<int> CreateUser(User user);
}

public class UserService : IUserService
{
    private readonly DataContext _dbContext;

    public UserService(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<User>> GetUsers()
    {
        return await _dbContext.User.ToListAsync();
    }
    
    public async Task<int> CreateUser(User user) {
        _dbContext.User.Add(user);

        return await _dbContext.SaveChangesAsync();
    }
}