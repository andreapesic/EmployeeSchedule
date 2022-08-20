using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeSchedule.Data.Entities
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("EmployeeId")]
        [ValidateNever]
        public Employee Employee { get; set; }
        [Required]
        public string ShiftWork { get; set; }
        public DateTime CheckInTime { get; set; }
        public bool Late { get; set; }
        public string Notification { get; set; }
    }
}
