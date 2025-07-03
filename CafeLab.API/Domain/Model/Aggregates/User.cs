using CafeLab.API.Domain.Model.Aggregates;
using System;
using System.ComponentModel.DataAnnotations;

namespace CafeLab.API.Domain.Model.Aggregates
{
    public class User : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Username { get; private set; }
        
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; private set; }
        
        [Required]
        public string PasswordHash { get; private set; }
        
        [Required]
        [StringLength(100)]
        public string FullName { get; private set; }
        
        [Required]
        [StringLength(20)]
        public string Role { get; private set; }
        
        public bool IsActive { get; private set; }
        public DateTime? LastLoginDate { get; private set; }
        public int LoginAttempts { get; private set; }
        public DateTime? LockoutEnd { get; private set; }
        public new DateTime CreatedAt { get; private set; }
        public new DateTime? UpdatedAt { get; private set; }
        public string CreatedBy { get; private set; }
        public string UpdatedBy { get; private set; }

        private User() { }

        public User(string username, string email, string passwordHash, string fullName, string role, string createdBy = "system")
        {
            if (string.IsNullOrWhiteSpace(username)) 
                throw new ArgumentException("Username cannot be empty.", nameof(username));
            if (string.IsNullOrWhiteSpace(email)) 
                throw new ArgumentException("Email cannot be empty.", nameof(email));
            if (string.IsNullOrWhiteSpace(passwordHash)) 
                throw new ArgumentException("Password hash cannot be empty.", nameof(passwordHash));
            if (string.IsNullOrWhiteSpace(fullName)) 
                throw new ArgumentException("Full name cannot be empty.", nameof(fullName));
            if (string.IsNullOrWhiteSpace(role)) 
                throw new ArgumentException("Role cannot be empty.", nameof(role));
            
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            FullName = fullName;
            Role = role;
            IsActive = true;
            LoginAttempts = 0;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;
        }

        public void SetPassword(string newPasswordHash)
        {
            if (string.IsNullOrWhiteSpace(newPasswordHash))
                throw new ArgumentException("Password hash cannot be empty.", nameof(newPasswordHash));
                
            PasswordHash = newPasswordHash;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetLastLoginDate()
        {
            LastLoginDate = DateTime.UtcNow;
            LoginAttempts = 0; // Reset login attempts on successful login
            UpdatedAt = DateTime.UtcNow;
        }

        public void IncrementLoginAttempts()
        {
            LoginAttempts++;
            if (LoginAttempts >= 5) // Lock after 5 failed attempts
            {
                LockoutEnd = DateTime.UtcNow.AddMinutes(15); // Lock for 15 minutes
            }
            UpdatedAt = DateTime.UtcNow;
        }

        public bool IsLockedOut()
        {
            return LockoutEnd.HasValue && LockoutEnd.Value > DateTime.UtcNow;
        }

        public void Unlock()
        {
            LoginAttempts = 0;
            LockoutEnd = null;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateProfile(string fullName, string email, string updatedBy)
        {
            if (!string.IsNullOrWhiteSpace(fullName))
                FullName = fullName;
            if (!string.IsNullOrWhiteSpace(email))
                Email = email;
            
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = updatedBy;
        }
    }
} 