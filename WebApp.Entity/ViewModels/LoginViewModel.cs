using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Entity.ViewModels
{
    public class LoginViewModel
    {
        [EmailAddress]
        public required string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
