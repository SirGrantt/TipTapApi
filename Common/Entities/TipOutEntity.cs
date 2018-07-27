using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Entities
{
    public class TipOutEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal TeamGrossSales { get; set; }
        public decimal FinalTeamBarSales { get; set; }
        public decimal SaTipOut { get; set; }
        public decimal BarTipOut { get; set; }
        public decimal BarBackTipOut { get; set; }
        public decimal BarBackCashTipOut { get; set; }
        public DateTime ShiftDate { get; set; }
        public int TeamId { get; set; }
        public string LunchOrDinner { get; set; }
        public string TeamType { get; set; }

        [Required]
        [ForeignKey("TeamId")]
        public TeamEntity Team { get; set; }
    }
}
