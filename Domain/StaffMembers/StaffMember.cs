using System;
using System.Collections.Generic;
using Domain.StaffEarnings;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Domain.Jobs;

namespace Domain.StaffMembers
{
    public class StaffMember
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Job> ApprovesJobs { get; set; }
        public List<Earnings> Earnings { get; set; }

        public StaffMember()
        {
            ApprovesJobs = new List<Job>();
            Earnings = new List<Earnings>();
        }
    }
}
