using System;

namespace Domain.Entities.Auth;

public class RefreshToken : BaseEntity
{
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserMember? UserMember { get; set; }
        public string? Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;

}
