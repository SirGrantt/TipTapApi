using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipTapApi.Entities;
using TipTapApi.Models;

namespace TipTapApi.Services.RoleServices
{
    public interface IJobRepository
    {
        Job CreateJob(JobDto role);
        IEnumerable<Job> GetJobs();
        Job GetJob(int jobId);
        void UpdateJob(int jobId, JobDto job);
        bool JobExists(int jobId);
        void DeleteJob(int jobId);
    }
}
