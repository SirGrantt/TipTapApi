using Common.DTOs.EarningsDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.CheckOutDtos
{
    public class ServerTeamGroupedCheckoutsDto
    {
        public int TeamId { get; set; }
        public bool IsSoloTeam { get; set; }
        public List<CheckoutOverviewDto> TeamCheckouts { get; set; }
        public EarningDto TeamEarning { get; set; }
        public bool CheckoutHasBeenRun { get; set; }

        public ServerTeamGroupedCheckoutsDto()
        {
            TeamCheckouts = new List<CheckoutOverviewDto>();
        }
    }
}
