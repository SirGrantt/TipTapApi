using Domain.Checkouts;
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
        decimal CalculateTeamGrossSales(List<Checkout> checkouts);
        decimal CalculateTeamBarSales(List<Checkout> checkouts);
        Earnings CalculateEarnings(List<Checkout> checkouts, TipOut tipout, DateTime shiftDate, string lunchOrDinner);
    }
}
