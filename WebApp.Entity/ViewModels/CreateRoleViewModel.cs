using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Entity.ViewModels
{
    public class CreateRoleViewModel
    {
        [Display(Name="Role Name")]
        [Required]
        public string Name { get; set; }
    }
}
