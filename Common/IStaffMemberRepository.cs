using Common.Entities;
using Domain.StaffMembers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public interface IStaffMemberRepository : IRepository
    {
        IEnumerable<StaffMemberEntity> GetStaffMembers();
        StaffMemberEntity GetStaffMember(int staffId);
        bool StaffMemberExists(int staffId);
        bool JobExists(int jobId);
        void AddStaffMember(StaffMemberEntity s);
        void DeleteStaffMember(StaffMemberEntity s);
        void SetStaffMemberMainJob(StaffMemberEntity s, JobEntity j);
    }
}
