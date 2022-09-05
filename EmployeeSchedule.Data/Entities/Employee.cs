using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeSchedule.Data.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required,StringLength(50,ErrorMessage ="Name must have at least 3 characters",MinimumLength =3)]
        public string Name { get; set; }
        [Required, StringLength(50, ErrorMessage = "Surname must have at least 3 characters", MinimumLength = 3)]
        public string Surname { get; set; }

        public string FullName { get { return Name + " " + Surname; } }

        [Required]
        public string Adress { get; set; }
        [Required, StringLength(50, ErrorMessage = "Number must have at least 7 characters", MinimumLength = 7)]
        public string Number { get; set; }
        [Required,EmailAddress]
        public string Email { get; set;  }
        [Required, StringLength(50, ErrorMessage = "Password must have at least 5 characters", MinimumLength = 5)]
        public string Password { get; set; }
        public string Possition { get; set; }
        [ForeignKey("CompanyId")]
        [ValidateNever]
        public Company Company { get; set; }
        public bool Administrator { get; set; }
        [Display(Name="City")]
        public string CityName { get; set; }

       
    }
}
