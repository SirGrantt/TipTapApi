using AutoMapper;
using Common.DTOs.JobDtos;
using Common.Entities;
using Common.RepositoryInterfaces;
using Common.Utilities;
using Domain.Jobs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class JobCore
    {
        private IJobRepository jobRepository;

        public JobCore(IJobRepository repository)
        {
            jobRepository = repository;
        }

        public bool JobExists(int jobId)
        {
            return jobRepository.JobExists(jobId);
        }

        public Job GetJob(int jobId)
        {
            Job job = Mapper.Map<Job>(jobRepository.GetJob(jobId));
            return job;
        }

        public JobDto GetJobByTitle(string title)
        {
            JobEntity job = jobRepository.GetJobByTitle(title);
            
            if (job == null)
            {
                throw new KeyNotFoundException("No Job with the provided Title was found");
            }

            return Mapper.Map<JobDto>(job);
        }

        public List<JobDto> GetAllJobs()
        {
            List<JobEntity> jobs = jobRepository.GetAllJobs();
            return Mapper.Map<List<JobDto>>(jobs);
        }

        public List<JobDto> GetStaffMemberJobs(int staffMemberId)
        {
            List<JobEntity> jobs = jobRepository.GetApprovedJobsForStaffMember(staffMemberId);
            return Mapper.Map<List<JobDto>>(jobs);
        }

        public void AddApprovedJobsToStaffMember(StaffMemberEntity staffMember, List<int> jobIds)
        {
            foreach (int id in jobIds)
            {
                if (!jobRepository.JobExists(id))
                {
                    throw new KeyNotFoundException($"No job with the ID of {id} was found");
                }
                else if (jobRepository.JobIsAssigned(id, staffMember.Id))
                {
                    throw new InvalidOperationException("You cannot add a job to a staff member who already has that approved job");
                }
                else
                {
                    JobEntity job = jobRepository.GetJob(id);
                    jobRepository.AddApprovedJobForStaffMember(job, staffMember);
                }
            }
            UtilityMethods.VerifyDatabaseSaveSuccess(jobRepository);
        }

        public void RemoveJobApproval(int staffMemberId, List<int> jobIds)
        {
            foreach (int id in jobIds)
            {
                if (!jobRepository.JobExists(id))
                {
                    throw new KeyNotFoundException($"No job with the id of {id} was found");
                }
                else if (!jobRepository.JobIsAssigned(id, staffMemberId))
                {
                    throw new InvalidOperationException("You cannot remove a job approval for a job that was never assigned.");
                }
                else
                {
                    jobRepository.RemoveApprovedJobFromStaffMember(id, staffMemberId);
                }
            }
            UtilityMethods.VerifyDatabaseSaveSuccess(jobRepository);
        }

    }
}
