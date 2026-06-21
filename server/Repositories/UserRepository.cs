using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Interfaces;
using server.Models;

namespace server.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LearnBudgetContext _context;

        public UserRepository(LearnBudgetContext context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetUsersList()
        {
            _logger.LogInformation("Database query: Retrieving all users");
            try
            {
                var result = await _context.Users.ToListAsync();
                _logger.LogInformation("Database query completed: Retrieved {UserCount} users", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error: Failed to retrieve users list");
                throw;
            }
        }

        public async Task<User?> GetUserById(string id)
        {
            _logger.LogInformation("Database query: Fetching user - UserId: {UserId}", id);
            try
            {
                var result = await _context.Users
                            .FirstOrDefaultAsync(u => u.Id == id);
             logger.LogInformation("Database operation: Creating new user - UserId: {UserId}, Email: {Email}", user.Id, user.Email);
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Database operation completed: User created successfully - UserId: {UserId}", user.Id);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error: Failed to create user - UserId: {UserId}", user.Id);
                throw;
            }ger.LogInformation("Database query completed: User found - UserId: {UserId}", id);
                }
                else
                {
                    _logger.LogInformation("Database query completed: User not found - UserId: {UserId}", id);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error: Failed to fetch user - UserId: {UserId}", id);
                throw;
            }
        }_logger.LogInformation("Database operation: Updating user - UserId: {UserId}", user.Id);
            try
            {
                var existing = await _context.Users.FindAsync(user.Id);
                if (existing == null)
                {
                    _logger.LogWarning("Database operation failed: User not found - UserId: {UserId}", user.Id);
                    return null;
                }
                _context.Entry(existing).CurrentValues.SetValues(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Database operation completed: User updated successfully - UserId: {UserId}", user.Id);
                return existing;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error: Failed to update user - UserId: {UserId}", user.Id);
                throw;
            }aveChangesAsync();
            _logger.LogInformation("Database operation: Deleting user - UserId: {UserId}", id);
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    _logger.LogWarning("Database operation failed: User not found - UserId: {UserId}", id);
                    return false;
                }
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Database operation completed: User deleted successfully - UserId: {UserId}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error: Failed to delete user - UserId: {UserId}", id);
                throw;
            } = await _context.Users.FindAsync(user.Id);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteUser(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) 
                return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
