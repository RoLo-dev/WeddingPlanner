using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner2.Models
{
    [NotMapped]
    public class Login
    {
        [Required]
        [Display(Name="Email:")]
        [EmailAddress]
        public string LogEmail {get;set;}

        [Required]
        [DataType(DataType.Password)]
        [Display(Name="Password:")]
        public string LogPassword {get;set;}
    }
}