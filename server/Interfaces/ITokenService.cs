namespace server.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string userId, string email, string fullName, UserRole role);
    }
}
