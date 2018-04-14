using Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.RepositoryInterfaces
{
    public interface IJobRepository : IRepository
    {
        bool JobExists(int jobId);
        JobEntity GetJob(int jobId);
        JobEntity GetJobByTitle(string title);
        List<JobEntity> GetAllJobs();
        List<JobEntity> GetApprovedJobsForStaffMember(int staffMemberId);
        void AddApprovedJobForStaffMember(JobEntity job, StaffMemberEntity staffMember);
        void RemoveApprovedJobFromStaffMember(int jobId, int staffMemberId);
        bool JobIsAssigned(int jobId, int staffMemberId);
    }
}
