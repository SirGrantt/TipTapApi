using Domain.Checkouts;
using Domain.TipOuts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using static Domain.Utilities.TipOutCalculator.JobTypeEnum;

namespace Domain.Utilities.TipOutCalculator
{
    public class TipOutCalculatorFactory
    {
        public Dictionary<JobType, Func<ITipOutCalculator>> TipOutCalculatorCreators { get; private set; }
        public TipOutCalculatorFactory()
        {
            TipOutCalculatorCreators = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => typeof(ITipOutCalculator).IsAssignableFrom(t) && t.IsInterface == false)
                .Select(t => new Func<ITipOutCalculator>(() => Activator.CreateInstance(t) as ITipOutCalculator))
                .ToDictionary(f => f().Job);
        }

        public ITipOutCalculator CreateCalculator(JobType job)
        {
            return TipOutCalculatorCreators[job]();
        }

        public Func<ITipOutCalculator> GetFactoryMethod(JobType job)
        {
            return TipOutCalculatorCreators[job];
        }
    }
}
