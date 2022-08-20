using System.ComponentModel.DataAnnotations;

namespace EmployeeSchedule.Data.Entities
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, StringLength(13, ErrorMessage = "Identification number must have 13 characters", MinimumLength = 13)]
        public string IdentificationNumber { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        public string Domain { get; set; }

    }
}
