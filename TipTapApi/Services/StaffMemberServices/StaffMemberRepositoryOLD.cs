using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipTapApi.Entities;
using TipTapApi.Models;

namespace TipTapApi.Services
{
    public class StaffMemberRepositoryOLD : IStaffMemberRepositoryOLD
    {
        private StaffMemberContextBAD _context;
        public StaffMemberRepositoryOLD(StaffMemberContextBAD context)
        {
            _context = context;
        }
        public void AddApprovedJob(Job role)
        {
            throw new NotImplementedException();
        }

        public void CreateStaffMember(StaffMemberOLD staffMember)
        {
            _context.StaffMembers.Add(staffMember);
        }

        public StaffMemberOLD GetStaffMember(int staffMemberId)
        {
            return _context.StaffMembers.FirstOrDefault(s => s.Id == staffMemberId);
        }

        public IEnumerable<StaffMemberOLD> GetStaffMembers()
        {
            return _context.StaffMembers.OrderBy(c => c.FirstName);
        }

        public void RemoveApprovedJob(int staffMemberId, int jobId)
        {
            throw new NotImplementedException();
        }

        public bool StaffMemberExists(int staffMemberId)
        {
            return _context.StaffMembers.Any(s => s.Id == staffMemberId);
        }

        public void UpdateStaffMember(StaffMemberOLD staffMember, int staffMemberId)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void DeleteStaffMember(StaffMemberOLD staffMember)
        {
            _context.Remove(staffMember);
        }
    }
}
