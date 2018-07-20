﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.CheckOutDtos
{
    public class CheckoutPagePresentationDto
    {
        public List<CheckoutOverviewDto> NotRunCheckouts { get; set; }
        public List<TeamGroupedCheckoutsDto> TeamCheckouts { get; set; }
        public TeamGroupedCheckoutsDto BarCheckouts { get; set; }

        public CheckoutPagePresentationDto()
        {
            TeamCheckouts = new List<TeamGroupedCheckoutsDto>();
            NotRunCheckouts = new List<CheckoutOverviewDto>();
            BarCheckouts = new TeamGroupedCheckoutsDto();
        }
    }
}
