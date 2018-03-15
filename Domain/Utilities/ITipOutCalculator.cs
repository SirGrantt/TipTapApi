using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Utilities
{
    public interface ITipOutCalculator
    {
        decimal CalculateTipOut(decimal target, decimal tipOutPercent);
    }
}
