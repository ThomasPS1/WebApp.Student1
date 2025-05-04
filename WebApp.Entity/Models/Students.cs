using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Student1.Models
{
    

        [Table("Students")]
        public class Students
        {
            [Key]
            public int StudentId { get; set; }

            public string? StudentName { get; set; }
            
            //public string? Location { get; set; }
            //list of enrollment
            public List<Enrollment>? Enrollment { get; set; }
        //1 grade

            public string? IdentityUserId { get; set; }
            [ForeignKey("IdentityUserId")]
            public IdentityUser? IdentityUser { get; set; }

        }
    }

