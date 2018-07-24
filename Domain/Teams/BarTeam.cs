using Domain.Checkouts;
using Domain.TipOuts;
using Domain.Utilities.TipOutCalculator;
using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Utilities.TipOutCalculator.JobTypeEnum;

namespace Domain.Teams
{
    public class BarTeam
    {
        public int Id { get; set; }
        public DateTime ShiftDate { get; set; }
        public string LunchOrDinner { get; set; }
        public List<Checkout> Checkouts { get; set; }
        public bool CheckoutHasBeenRun { get; set; }
        public TipOut TipOut { get; set; }
        public ITipOutCalculator TipOutCalculator { get; set; }

        public BarTeam(DateTime shiftDate)
        {
            ShiftDate = shiftDate;
            Checkouts = new List<Checkout>();
            TipOutCalculatorFactory factory = new TipOutCalculatorFactory();
            TipOutCalculator = factory.CreateCalculator(JobType.Server);
            TipOut = new TipOut(shiftDate);

        }
    }
}
