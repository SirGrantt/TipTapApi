using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.CheckOutDtos
{
    public class ServerCheckoutPagePresentationDto
    {
        public List<CheckoutOverviewDto> NotRunCheckouts { get; set; }
        public List<TeamGroupedCheckoutsDto> TeamCheckouts { get; set; }

        public ServerCheckoutPagePresentationDto()
        {
            TeamCheckouts = new List<TeamGroupedCheckoutsDto>();
            NotRunCheckouts = new List<CheckoutOverviewDto>();
        }
    }
}
