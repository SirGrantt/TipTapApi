using AutoMapper;
using Common;
using Common.Entities;
using Domain.StaffMembers;
using Persistence.Contexts;
using Persistence.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class StaffMemberRepository : IStaffMemberRepository
    {
        private ShiftContext _context;
        public StaffMemberRepository(ShiftContext context)
        {
            _context = context;
        }

        public void AddStaffMember(StaffMemberEntity s)
        {
            _context.Add(s);
        }

        public void DeleteStaffMember(StaffMemberEntity sm)
        {
            _context.Remove(sm);
        }

        public StaffMemberEntity GetStaffMember(int staffId)
        {
            return _context.StaffMembers.FirstOrDefault(s => s.Id == staffId);
        }

        public IEnumerable<StaffMemberEntity> GetStaffMembers()
        {
            return _context.StaffMembers.OrderBy(s => s.FirstName);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool StaffMemberExists(int staffId)
        {
            return _context.StaffMembers.Any(s => s.Id == staffId);
        }
    }
}
