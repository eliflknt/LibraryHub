using LibraryHub.Domain.Common;

namespace LibraryHub.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public int? MemberId { get; set; }

    public Member? Member { get; set; }
}