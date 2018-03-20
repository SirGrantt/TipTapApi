using Common.DTOs.JobDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.TeamDtos
{
    public class ServerTeamDto
    {
        public int Id { get; set; }
        public bool CheckoutHasBeenRun { get; set; }
        public List<ServerDto> Servers { get; set; }
        public decimal IndividualFinalCcTips { get; set; }
        public decimal TeamCcTipsAfterCheckout { get; set; }
        public decimal IndividualFinalAutoGratTakeHome { get; set; }
        public decimal IndividualFinalCCandAutoGratTakeHome { get; set; }
        public decimal IndividualFinalCashTakeHome { get; set; }
        public decimal BarTipOut { get; set; }
        public decimal SATipOut { get; set; }
        public int NumberOfBottlesSold { get; set; }
    }
}
