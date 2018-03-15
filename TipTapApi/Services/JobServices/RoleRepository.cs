using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipTapApi.Entities;
using TipTapApi.Models;

namespace TipTapApi.Services.RoleServices
{
    public class RoleRepository : IJobRepository
    {
        private IJobRepository _context;
        public RoleRepository(IJobRepository context)
        {
            _context = context;
        }

        public Job CreateJob(JobDto role)
        {
            throw new NotImplementedException();
        }

        public void DeleteJob(int jobId)
        {
            throw new NotImplementedException();
        }

        public Job GetJob(int jobId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Job> GetJobs()
        {
            throw new NotImplementedException();
        }

        public bool JobExists(int jobId)
        {
            throw new NotImplementedException();
        }

        public void UpdateJob(int jobId, JobDto job)
        {
            throw new NotImplementedException();
        }
    }
}
