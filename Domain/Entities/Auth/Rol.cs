using System;

namespace Domain.Entities.Auth;

public class Rol : BaseEntity
{
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<UserMember> UsersMembers { get; set; } = new HashSet<UserMember>();
        public ICollection<UserMemberRol> UserMemberRols { get; set; } = new HashSet<UserMemberRol>();
}
