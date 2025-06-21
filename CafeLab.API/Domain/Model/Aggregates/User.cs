using CafeLab.API.Domain.Model.Aggregates;
using System;

namespace CafeLab.API.Domain.Model.Aggregates
{
    public class User : BaseEntity
    {
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string FullName { get; private set; }
        public string Role { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime? LastLoginDate { get; private set; }

        private User() { }

        public User(string username, string email, string passwordHash, string fullName, string role)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Username cannot be empty.", nameof(username));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email cannot be empty.", nameof(email));
            
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            FullName = fullName;
            Role = role;
            IsActive = true;
        }

        public void SetPassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash;
        }

        public void SetLastLoginDate()
        {
            LastLoginDate = DateTime.UtcNow;
        }
    }
} 