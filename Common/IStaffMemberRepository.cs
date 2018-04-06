using Common.Entities;
using Domain.StaffMembers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public interface IStaffMemberRepository
    {
        IEnumerable<StaffMemberEntity> GetStaffMembers();
        StaffMemberEntity GetStaffMember(int staffId);
        bool StaffMemberExists(int staffId);
        bool JobExists(int jobId);
        void AddStaffMember(StaffMemberEntity s);
        void DeleteStaffMember(StaffMemberEntity s);
        bool Save();
        void SetStaffMemberMainJob(StaffMemberEntity s, JobEntity j);
    }
}
