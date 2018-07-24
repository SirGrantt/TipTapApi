using Common.DTOs.EarningsDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.CheckOutDtos
{
    public class TeamGroupedCheckoutsDto
    {
        public int TeamId { get; set; }
        public List<CheckoutOverviewDto> TeamCheckouts { get; set; }
        public bool CheckoutHasBeenRun { get; set; }
        public List<EarningDto> TeamEarnings { get; set; }

        public TeamGroupedCheckoutsDto()
        {
            TeamCheckouts = new List<CheckoutOverviewDto>();
            TeamEarnings = new List<EarningDto>();
        }
    }
}
