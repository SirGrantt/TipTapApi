using AutoMapper;
using Common.DTOs.JobDtos;
using Common.Entities;
using Common.RepositoryInterfaces;
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
                return null;
            }

            return Mapper.Map<JobDto>(job);
        }

    }
}
