using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Entities
{
    public class CheckoutEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime ShiftDate { get; set; }
        public decimal NonTipOutBarSales { get; set; }
        public int NumberOfBottlesSold { get; set; }
        public string LunchOrDinner { get; set; }
        public decimal Sales { get; set; }
        public decimal GrossSales { get; set; }
        public decimal BarSales { get; set; }
        public decimal CcTips { get; set; }
        public decimal CashTips { get; set; }
        public decimal CcAutoGrat { get; set; }
        public decimal CashAutoGrat { get; set; }
        public decimal Hours { get; set; }
        public int StaffMemberId { get; set; }
        public int JobWorkedId { get; set; }

        [Required]
        [ForeignKey("StaffMemberId")]
        public StaffMemberEntity StaffMember { get; set; }

        [Required]
        [ForeignKey("JobWorkedId")]
        public JobEntity Job { get; set; }

    }
}
