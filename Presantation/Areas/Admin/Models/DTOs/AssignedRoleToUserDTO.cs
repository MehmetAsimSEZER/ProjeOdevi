using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Presantation.Areas.Admin.Models.DTOs
{
    public class AssignedRoleToUserDTO
    {
        public IdentityRole Role { get; set; }

        public IEnumerable<User> HasRole { get; set; }
        public IEnumerable<User> HasNoRole { get; set; }

        public string RoleName { get; set; }

        public string[] AddIds { get; set; }
        public string[] RemoveIds { get; set; }
    }
}
