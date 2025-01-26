namespace DigitalWalletAPI.Domain.Entities
{
    public class User
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public ICollection<Wallet> Wallets { get; set; } = [];

        public User() { }

        public User(string name, string email, string passwordHash)
        {
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException("Name cannot be empty.");
            Email = !string.IsNullOrWhiteSpace(email) ? email : throw new ArgumentException("Email cannot be empty.");
            PasswordHash = !string.IsNullOrWhiteSpace(passwordHash) ? passwordHash : throw new ArgumentException("PasswordHash cannot be empty.");
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateName(string newName)
        {
            Name = !string.IsNullOrWhiteSpace(newName) ? newName : throw new ArgumentException("Name cannot be empty.");
        }

        public void UpdateEmail(string newEmail)
        {
            Email = !string.IsNullOrWhiteSpace(newEmail) ? newEmail : throw new ArgumentException("Email cannot be empty.");
        }

        //public bool VerifyPassword(string password, IPasswordHasher passwordHasher)
        //{
        //    return passwordHasher.VerifyHash(password, PasswordHash);
        //}
    }
}
