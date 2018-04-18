using System;
using System.ComponentModel.DataAnnotations;
namespace planner.Models
{
    public class PlanViewModel : BaseEntity
    {
        [Required]
        public string Wedder1 { get; set; }

        [Required]
        public string Wedder2 {get;set;}

        [Required]
        [DataType(DataType.Date)]
        [CurrentDate(ErrorMessage = "Weddings must be planned for the future!")]
        public DateTime Date { get; set; }

        [Required]
        public string Address { get; set; }
    }
}