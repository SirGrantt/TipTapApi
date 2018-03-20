using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Entities
{
    public class ServerEntity
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal GrossSales { get; set; }
        public decimal Sales { get; set; }
        public decimal BarSales { get; set; }
        public decimal CcTips { get; set; }
        public decimal CashTips { get; set; }
        public decimal CcAutoGratuity { get; set; }
        public decimal CashAutoGratuity { get; set; }
        public decimal NonTipoutBarSales { get; set; }
        public decimal Hours { get; set; }
        public decimal CcTipTakehome { get; set; }
        public decimal CashTipTakeHome { get; set; }
        public decimal AutoGratTakeHome { get; set; }
        public decimal TotalTipTakehome { get; set; }

        public int ServerTeamId { get; set; }
        public int StaffMemberId { get; set; }

        [Required]
        [ForeignKey("StaffMemberId")]
        public StaffMemberEntity StaffMember { get; set; }

        [Required]
        [ForeignKey("ServerTeamId")]
        public ServerTeamEntity ServerTeam { get; set; }

    }
}
