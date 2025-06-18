namespace CafeLab.API.Profiles.Domain.Model.Aggregates;

public partial class Profile
{
    public int Id { get; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string CafeteriaName { get; set; }
    public string Experience { get; set; }
    public string ProfilePicture { get; set; }
    public string PaymentMethod { get; set; }
    public bool IsFirstLogin { get; set; }
    public string Plan { get; set; }
    public bool HasPlan  { get; set; }
}