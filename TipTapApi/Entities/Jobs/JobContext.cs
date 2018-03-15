using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TipTapApi.Entities
{
    public class JobContext : DbContext
    {
        public JobContext(DbContextOptions options)
            :base(options)
        {

        }

        DbSet<Job> Jobs { get; set; }
    }
}
