using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipTapApi.Entities;
using TipTapApi.Models;

namespace TipTapApi.Services
{
    public interface IStaffMemberRepositoryOLD
    {
        void CreateStaffMember(StaffMemberOLD staffMember);
        void UpdateStaffMember(StaffMemberOLD staffMember, int staffMemberId);
        IEnumerable<StaffMemberOLD> GetStaffMembers();
        StaffMemberOLD GetStaffMember(int staffMemberId);
        void AddApprovedJob(Job role);
        void RemoveApprovedJob(int staffMemberId, int jobId);
        bool StaffMemberExists(int staffMemberId);
        bool Save();
        void DeleteStaffMember(StaffMemberOLD staffMember);

    }
}
