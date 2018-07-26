using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Entities
{
    public class SupportHoursEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal HoursWorked { get; set; }
        public DateTime ShiftDate { get; set; }
        public string LunchOrDinner { get; set; }

        [Required]
        [ForeignKey("StaffMemberId")]
        public int StaffMemberId { get; set; }

        [Required]
        [ForeignKey("JobWorkedId")]
        JobEntity JobWorked { get; set; }
    }
}
