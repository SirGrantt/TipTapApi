using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Entities
{
    public class EarningsEntity
    {
        [Required]
        public DateTime ShiftDate { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string JobWorked { get; set; }

        public bool OwedCashForTipOut { get; set; }
        public decimal CashPayedForTipOut { get; set; }
        public decimal CcTips { get; set; }
        public decimal AutoGratuity { get; set; }
        public decimal TotalTipsForPayroll { get; set; }
        public decimal CashTips { get; set; }

        public int StaffMemberId { get; set; }

        [Required]
        [ForeignKey("StaffMemberId")]
        public StaffMemberEntity StaffMember { get; set; }


    }
}
