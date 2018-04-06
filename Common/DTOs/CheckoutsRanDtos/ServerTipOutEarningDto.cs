using Common.DTOs.EarningsDtos;
using Common.DTOs.TipOutDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.CheckoutsRanDtos
{
    public class ServerTipOutEarningDto
    {
        public TipOutDto TeamTipOut { get; set; }
        public List<EarningDto> ServerEarnings { get; set; }
    }
}
