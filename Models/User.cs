namespace trainee_projectmanagement.Models;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string SchoolAttended { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string CoursesInterested { get; set; } = string.Empty; // JSON or CSV format
    public bool InterestedInCertification { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
