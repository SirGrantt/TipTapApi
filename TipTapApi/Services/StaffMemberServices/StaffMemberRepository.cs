using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipTapApi.Entities;
using TipTapApi.Models;

namespace TipTapApi.Services
{
    public class StaffMemberRepository : IStaffMemberRepository
    {
        private StaffMemberContext _context;
        public StaffMemberRepository(StaffMemberContext context)
        {
            _context = context;
        }
        public void AddApprovedRole(Role role)
        {
            throw new NotImplementedException();
        }

        public void CreateStaffMember(StaffMember staffMember)
        {
            _context.StaffMembers.Add(staffMember);
        }

        public StaffMember GetStaffMember(int staffMemberId)
        {
            return _context.StaffMembers.FirstOrDefault(s => s.Id == staffMemberId);
        }

        public IEnumerable<StaffMember> GetStaffMembers()
        {
            return _context.StaffMembers.OrderBy(c => c.FirstName);
        }

        public void RemoveApprovedRole(int staffMemberId, int roleId)
        {
            throw new NotImplementedException();
        }

        public bool StaffMemberExists(int staffMemberId)
        {
            return _context.StaffMembers.Any(s => s.Id == staffMemberId);
        }

        public void UpdateStaffMember(StaffMember staffMember, int staffMemberId)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void DeleteStaffMember(StaffMember staffMember)
        {
            _context.Remove(staffMember);
        }
    }
}
