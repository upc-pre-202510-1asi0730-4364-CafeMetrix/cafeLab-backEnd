using System;

namespace CafeLab.API.Application.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string username) : base($"User with username '{username}' already exists.")
        {
        }
    }
} 