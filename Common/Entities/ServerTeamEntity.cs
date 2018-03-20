using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Entities
{
    public class ServerTeamEntity
    {
        public int Id { get; set; }
        public bool CheckoutHasBeenRun { get; set; }
        public List<ServerEntity> Servers { get; set; }
        public decimal IndividualFinalCcTips { get; set; }
        public decimal TeamCcTipsAfterCheckout { get; set; }
        public decimal IndividualFinalAutoGratTakeHome { get; set; }
        public decimal IndividualFinalCCandAutoGratTakeHome { get; set; }
        public decimal IndividualFinalCashTakeHome { get; set; }
        public decimal BarTipOut { get; set; }
        public decimal SATipOut { get; set; }
        public int NumberOfBottlesSold { get; set; }

        public int ServerGroupId { get; set; }

        [Required]
        [ForeignKey("ServerGroupId")]
        public ServerGroupEntity ServerGroup { get; set; }
    }
}
