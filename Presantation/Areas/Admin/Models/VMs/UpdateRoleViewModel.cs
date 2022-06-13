using System.ComponentModel.DataAnnotations;

namespace Presantation.Areas.Admin.Models.VMs
{
    public class UpdateRoleViewModel
    {
        public UpdateRoleViewModel()
        {
            Users = new List<string>();
        }
        public string Id { get; set; }
        [Required(ErrorMessage = "Role Name is required")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}
