using Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.RepositoryInterfaces
{
    public interface IJobRepository
    {
        bool JobExists(int jobId);
        JobEntity GetJob(int jobId);
        JobEntity GetJobByTitle(string title);
    }
}
