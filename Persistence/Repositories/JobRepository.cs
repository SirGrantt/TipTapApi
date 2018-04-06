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

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
