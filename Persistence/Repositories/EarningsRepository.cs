using Common.Entities;
using Common.RepositoryInterfaces;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence.Repositories
{
    public class EarningsRepository : IEarningsRepository
    {
        private CheckoutManagerContext _context;
        public EarningsRepository(CheckoutManagerContext context)
        {
            _context = context;
        }

        public void AddEarning(EarningsEntity earning)
        {
            _context.Earnings.Add(earning);
        }

        public void DeleteEarning(int earningsId)
        {
            EarningsEntity earningToRemove = GetEarningById(earningsId);
            _context.Earnings.Remove(earningToRemove);
        }

        public bool EarningExists(int staffMemberId, DateTime shiftDate, string lunchOrDinner)
        {
            return _context.Earnings.Any(e => e.StaffMemberId == staffMemberId && e.ShiftDate == shiftDate
            && e.LunchOrDinner == lunchOrDinner);
        }

        public bool EarningExistsById(int earningId)
        {
            return _context.Earnings.Any(e => e.Id == earningId);
        }

        public EarningsEntity GetEarning(int staffMemberId, DateTime shiftDate, string lunchOrDinner)
        {
            return _context.Earnings.Where(e => e.StaffMemberId == staffMemberId && e.ShiftDate == shiftDate && e.LunchOrDinner == lunchOrDinner.ToLower().Trim()).FirstOrDefault();
        }

        public EarningsEntity GetEarningById(int earningId)
        {
            return _context.Earnings.FirstOrDefault(e => e.Id == earningId);
        }

        public IEnumerable<EarningsEntity> GetEarningsForAStaffMemberBetweenDates(int staffId, DateTime startDate, DateTime endDate)
        {
            List<EarningsEntity> earnings = new List<EarningsEntity>();
            foreach (EarningsEntity e in _context.Earnings.Where(e => e.ShiftDate >= startDate 
            && e.ShiftDate <= endDate && e.StaffMemberId == staffId))
            {
                earnings.Add(e);
            }

            return earnings;
        }

        public IEnumerable<EarningsEntity> GetEarningsForShift(DateTime shiftDate, string lunchOrDinner)
        {
            List<EarningsEntity> earnings = new List<EarningsEntity>();
            foreach (EarningsEntity e in _context.Earnings.Where(e => e.ShiftDate == shiftDate && e.LunchOrDinner == lunchOrDinner))
            {
                earnings.Add(e);
            }

            return earnings;
        }

        public void ResetEarning(EarningsEntity earning)
        {
            _context.Earnings.Remove(earning);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
