using AutoMapper;
using Common;
using Common.Entities;
using Domain.StaffMembers;
using Persistence.Contexts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class StaffMemberRepository : IStaffMemberRepository
    {
        private CheckOutManagerContext _context;
        public StaffMemberRepository(CheckOutManagerContext context)
        {
            _context = context;
        }

        public void AddStaffMember(StaffMemberEntity s)
        {
            _context.StaffMembers.Add(s);
        }

        public void DeleteStaffMember(StaffMemberEntity sm)
        {
            _context.StaffMembers.Remove(sm);
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
