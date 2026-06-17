using server.Interfaces;
using server.Models;
using static server.DTOs.AuthDto;
using static server.DTOs.UserDTOs;

namespace server.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IEmployeeRepository employeeRepository, ITokenService tokenService, IConfiguration configuration)
        { 
            _userRepository = userRepository;
            _employeeRepository = employeeRepository;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        public async Task<IEnumerable<UserResponseDto>> GetUsersList()
        {
            var users = await _userRepository.GetUsersList();
            return users.Select(MapToResponseDto);
        }

        public async Task<UserResponseDto?> GetUserById(string id)
        {
            var user = await _userRepository.GetUserById(id);
            return user != null ? MapToResponseDto(user) : null;
        }

        public async Task<LoginResponseDto?> Authenticate(string id, string password)
        {
            var user = await _userRepository.GetUserById(id);

            if (user == null)
            {
                //_logger.LogWarning("Login attempt failed: User not found for email {Email}", id);
                return null;
            }

            // Verify password (simplified - in production use proper password verification)
            var hashedPassword = HashPassword(password);
            if (user.Password != hashedPassword)
            {
               // _logger.LogWarning("Login attempt failed: Invalid password for email {Email}", id);
                return null;
            }

            var token = _tokenService.GenerateToken(user.Id, user.Email, $"{user.FirstName} {user.LastName}", user.Role);            var expiryMinutes = _configuration.GetValue<int>("JwtSettings:ExpiryMinutes", 70);

            //_logger.LogInformation("User {UserId} authenticated successfully", user.Id);

            return new LoginResponseDto
            {
                Token = token,
                TokenType = "Bearer",
                ExpiresIn = expiryMinutes * 60, // Convert to seconds
                User = MapToResponseDto(user)
            };
        }

        public async Task<UserResponseDto?> CreateUser(UserCreateDto user)
        {
            var existingUser = await _userRepository.GetUserById(user.Id);
            
            if (existingUser != null) 
                return null;

            var newUser = new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = UserRole.Employee,
                Phone = user.Phone,
                Password = HashPassword(user.Password),
            };
            var createdUser = await _userRepository.CreateUser(newUser);

            var newEmployee = new Employee
            {
                User = createdUser,
                Department = "כללי",
                TotalCredits = 20, 
                UsedCredits = 0
            };
            
            await _employeeRepository.CreateEmployeeAsync(newEmployee);

            return MapToResponseDto(createdUser);

        }

        public async Task<UserResponseDto?> UpdateUser(string id, UserUpdateDto updateUser)
        {
            var curUser = await _userRepository.GetUserById(id);
            if (curUser == null)
                return null;
            if (!string.IsNullOrWhiteSpace(updateUser.FirstName)) curUser.FirstName = updateUser.FirstName;
            if (!string.IsNullOrWhiteSpace(updateUser.LastName)) curUser.LastName = updateUser.LastName;
            if(updateUser.Email != null) curUser.Email = updateUser.Email;
            if(updateUser.Phone != null) curUser.Phone = updateUser.Phone;

            var updatedUser = await _userRepository.UpdateUser(curUser);
            return updatedUser != null ? MapToResponseDto(updatedUser) : null;
        }

        public async Task<bool> DeleteUser(string id)
        {
            return await _userRepository.DeleteUser(id);
        }

        private static UserResponseDto MapToResponseDto(User user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Role = user.Role
            };
        }

        private static string HashPassword(string password)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}
