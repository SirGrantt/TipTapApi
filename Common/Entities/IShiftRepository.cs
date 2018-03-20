using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entities
{
    public interface IShiftRepository
    {
        void Add(ShiftEntity t);
        void Delete(ShiftEntity t);
        bool Exists(DateTime date, string lunchOrDinner);
        ShiftEntity Get(DateTime date, string lunchOrDinner);
        IEnumerable<ShiftEntity> GetAll();
        IEnumerable<ShiftEntity> GetFromRange(DateTime startDate, DateTime endDate);
        bool Save();

    }
}
