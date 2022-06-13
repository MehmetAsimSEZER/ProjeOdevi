using System.ComponentModel.DataAnnotations;

namespace Presantation.Areas.Admin.Models.VMs
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
