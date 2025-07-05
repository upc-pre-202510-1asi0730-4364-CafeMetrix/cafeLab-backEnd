namespace CafeLab.API.Profiles.Interfaces.REST.Resources;

public record UpdateProfileResource(
    string Name,
    string Email,
    string Role,
    string CafeteriaName,
    string Experience,
    string ProfilePicture,
    string PaymentMethod,
    string Plan,
    bool HasPlan
);