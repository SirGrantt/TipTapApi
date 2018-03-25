using Domain.CheckOuts;
using Domain.StaffEarnings;
using Domain.TipOuts;
using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Utilities.TipOutCalculator.JobTypeEnum;

namespace Domain.Utilities.TipOutCalculator
{
    public interface ITipOutCalculator
    {
        JobType Job { get; }
        decimal CalculateTipOut(decimal target, decimal tipOutPercent, decimal specialLine);
        decimal CalculateTeamGrossSales(List<CheckOut> checkouts);
        decimal CalculateTeamBarSales(List<CheckOut> checkouts);
        Earnings CalculateEarnings(List<CheckOut> checkouts, TipOut tipout, DateTime shiftDate);
    }
}
