using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipTapApi.Entities;
using TipTapApi.Models;

namespace TipTapApi.Services
{
    public interface IStaffMemberRepository
    {
        void CreateStaffMember(StaffMember staffMember);
        void UpdateStaffMember(StaffMember staffMember, int staffMemberId);
        IEnumerable<StaffMember> GetStaffMembers();
        StaffMember GetStaffMember(int staffMemberId);
        void AddApprovedRole(Role role);
        void RemoveApprovedRole(int staffMemberId, int role);
        bool StaffMemberExists(int staffMemberId);
        bool Save();
        void DeleteStaffMember(StaffMember staffMember);

    }
}
