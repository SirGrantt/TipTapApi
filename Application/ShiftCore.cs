using AutoMapper;
using Common.Entities;
using Domain.ShiftManager;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class ShiftCore
    {
        IShiftRepository _repo;

        public ShiftCore(IShiftRepository repo)
        {
            _repo = repo;
        }

        public Shift GetShift(DateTime shiftDate, string lunchOrDinner)
        {
            if (!_repo.Exists(shiftDate, lunchOrDinner))
            {
                return null;
            }

            ShiftEntity shiftEntity = _repo.Get(shiftDate, lunchOrDinner);

            Shift shift = Mapper.Map<Shift>(shiftEntity);
            return shift;
        }

        public IEnumerable<Shift> GetShiftsFromRange(DateTime startDate, DateTime endDate)
        {
            var shifts = Mapper.Map<IEnumerable<Shift>>(_repo.GetFromRange(startDate, endDate));
            return shifts;
        }

        public Shift CreateShift(DateTime date, string lunchOrDinner)
        {
            if (_repo.Exists(date, lunchOrDinner))
            {
                throw new InvalidOperationException("A shift with that date and lunch or dinner specification already exists");
            }
            Shift shift = new Shift(date, lunchOrDinner.ToLower());
            ShiftEntity shiftToSave = Mapper.Map<ShiftEntity>(shift);
            _repo.Add(shiftToSave);

            if (!_repo.Save())
            {
                throw new Exception("An error occured while adding the shift");
            }

            return shift;
        }
    }
}
