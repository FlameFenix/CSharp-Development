namespace FootballManager.Services.PasswordHash
{
    public interface IPasswordHasher
    {
        public string HashPassword(string password);
    }
}
