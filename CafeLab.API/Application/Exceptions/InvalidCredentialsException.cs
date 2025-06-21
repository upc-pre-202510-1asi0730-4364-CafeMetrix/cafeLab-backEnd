using System;

namespace CafeLab.API.Application.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() : base("Invalid credentials provided.")
        {
        }
    }
} 