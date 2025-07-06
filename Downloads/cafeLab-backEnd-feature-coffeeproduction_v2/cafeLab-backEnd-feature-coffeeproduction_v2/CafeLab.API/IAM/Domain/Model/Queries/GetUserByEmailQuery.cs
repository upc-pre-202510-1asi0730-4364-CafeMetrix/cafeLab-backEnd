using CafeLab.API.IAM.Domain.Model.ValueObjects;

namespace CafeLab.API.IAM.Domain.Model.Queries;

/// <summary>
/// Query to get a user by email
/// </summary>
/// <param name="Email">Email address</param>
public record GetUserByEmailQuery(EmailAddress Email); 