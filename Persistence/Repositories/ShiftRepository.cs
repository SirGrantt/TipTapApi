using Common.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Persistence.Repositories
{
    public class ShiftRepository : IShiftRepository
    {
        ShiftContext _context;
        public ShiftRepository(ShiftContext context)
        {
            _context = context;
        }

        public void Add(ShiftEntity t)
        {
            _context.Shifts.Add(t);
        }

        public void Delete(ShiftEntity t)
        {
            _context.Shifts.Remove(t);
        }

        public bool Exists(DateTime date, string lunchOrDinner)
        {
             return _context.Shifts.Any(s => s.ShiftDate == date && s.LunchOrDinner.ToLower() == lunchOrDinner.ToLower());
        }

        public ShiftEntity Get(DateTime date, string lunchOrDinner)
        {
            return _context.Shifts.Where(s => s.ShiftDate == date && s.LunchOrDinner.ToLower() == lunchOrDinner.ToLower())
                .Include(s => s.ServerGroup)
                .ThenInclude(s => s.ServerTeams)
                .ThenInclude(s => s.Servers)
                .ThenInclude(s => s.StaffMember)
                .FirstOrDefault();            
        }

        public IEnumerable<ShiftEntity> GetAll()
        {
            return _context.Shifts.ToList();
        }

        
        public IEnumerable<ShiftEntity> GetFromRange(DateTime startDate, DateTime endDate)
        {
            var shifts = _context.Shifts.Where(s => s.ShiftDate >= startDate && s.ShiftDate <= endDate)
                .Include(s => s.ServerGroup)
                .ThenInclude(s => s.ServerTeams)
                .ThenInclude(s => s.Servers);
            return shifts;
        }
        
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
