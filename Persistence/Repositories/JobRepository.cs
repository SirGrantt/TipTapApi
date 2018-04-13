using Common.Entities;
using Common.RepositoryInterfaces;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence.Repositories
{
    public class JobRepository : IJobRepository
    {
        private CheckoutManagerContext _context;

        public JobRepository(CheckoutManagerContext context)
        {
            _context = context;
        }

        public void AddApprovedJobForStaffMember(JobEntity job, StaffMemberEntity staffMember)
        {
            ApprovedJobEntity approval = new ApprovedJobEntity
            {
                Job = job,
                StaffMember = staffMember
            };
            _context.ApprovedRoles.Add(approval);
        }

        public List<JobEntity> GetAllJobs()
        {
            return _context.Jobs.ToList();
        }

        public List<JobEntity> GetApprovedJobsForStaffMember(int staffMemberId)
        {
            List<JobEntity> approvedJobs = new List<JobEntity>();
            List<ApprovedJobEntity> reference = new List<ApprovedJobEntity>();

            foreach (ApprovedJobEntity approvedJob in _context.ApprovedRoles.Where(a => a.StaffMemberId == staffMemberId))
            {
                reference.Add(approvedJob);
            }

            foreach (ApprovedJobEntity r in reference)
            {
                JobEntity job = _context.Jobs.Where(j => j.Id == r.JobId).FirstOrDefault();
                approvedJobs.Add(job);
            }

            return approvedJobs;
        }

        public JobEntity GetJob(int jobId)
        {
            return _context.Jobs.FirstOrDefault(j => j.Id == jobId);
        }

        public JobEntity GetJobByTitle(string title)
        {
            return _context.Jobs.FirstOrDefault(j => j.Title.ToLower() == title.ToLower());
        }

        public bool JobExists(int jobId)
        {
            return _context.Jobs.Any(j => j.Id == jobId);
        }

        public void RemoveApprovedJobFromStaffMember(int jobId, int staffMemberId)
        {
            ApprovedJobEntity approvedJob = _context.ApprovedRoles.Where(j => j.StaffMemberId == staffMemberId && j.JobId == jobId).FirstOrDefault();
            _context.ApprovedRoles.Remove(approvedJob);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
