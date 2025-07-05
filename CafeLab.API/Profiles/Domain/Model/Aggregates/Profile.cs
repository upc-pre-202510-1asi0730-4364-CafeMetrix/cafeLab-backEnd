using CafeLab.API.Profiles.Domain.Model.Commands;
using CafeLab.API.Profiles.Domain.Model.ValueObjects;

namespace CafeLab.API.Profiles.Domain.Model.Aggregates;

/// <summary>
/// Profile Aggregate Root 
/// </summary>
/// <remarks>
/// This class represents the Profile aggregate root.
/// It contains the properties and methods to manage the profile information.
/// </remarks>

public partial class Profile
{
    public int Id { get; }
    
    public string Name { get; private set; }
    public EmailAddress Email { get; private set; }
    public string Password { get; private set; }
    public string Role { get; private set; }
    public string CafeteriaName { get; private set; }
    public string Experience { get; private set; }
    public string ProfilePicture { get; private set; }
    public string PaymentMethod { get; private set; }
    public bool IsFirstLogin { get; private set; }
    public string Plan { get; private set; }
    public bool HasPlan  { get; private set; }
    
    public string EmailAddress => Email.Address;

    public Profile()
    {
        Email = new EmailAddress();
    }
    
    public Profile(string name , string email, string password, string role, string cafeteriaName, string experience, string profilePicture, string paymentMethod, bool isFirstLogin, string plan, bool hasPlan)
    {
        // El Id será asignado por la base de datos (Identity)
        Name = name;
        Email = new EmailAddress(email);
        Password = password;
        Role = role;
        CafeteriaName = cafeteriaName;
        Experience = experience;
        ProfilePicture = profilePicture;
        PaymentMethod = paymentMethod;
        IsFirstLogin = isFirstLogin;
        Plan = plan;
        HasPlan = hasPlan;
    }

    public Profile(CreateProfileCommand command)
    {
        Name = command.Name;
        Email = new EmailAddress(command.Email);
        Password = command.Password;
        Role = command.Role;
        CafeteriaName = command.CafeteriaName;
        Experience = command.Experience;
        ProfilePicture = command.ProfilePicture;
        PaymentMethod = command.PaymentMethod;
        IsFirstLogin = command.IsFirstLogin;
        Plan = command.Plan;
        HasPlan = command.HasPlan;
    }
    
    public void Update(
        string name,
        string email,
        string role,
        string cafeteriaName,
        string experience,
        string profilePicture,
        string paymentMethod,
        string plan,
        bool hasPlan
    )
    {
        Name = name;
        Email = new EmailAddress(email);
        Role = role;
        CafeteriaName = cafeteriaName;
        Experience = experience;
        ProfilePicture = profilePicture;
        PaymentMethod = paymentMethod;
        Plan = plan;
        HasPlan = hasPlan;
    }
}