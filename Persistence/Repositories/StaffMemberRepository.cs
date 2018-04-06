using AutoMapper;
using Common;
using Common.Entities;
using Domain.StaffMembers;
using Persistence.Contexts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class StaffMemberRepository : IStaffMemberRepository
    {
        private CheckoutManagerContext _context;
        public StaffMemberRepository(CheckoutManagerContext context)
        {
            _context = context;
        }

        public void AddStaffMember(StaffMemberEntity s)
        {
            s.Status = "active";
            _context.StaffMembers.Add(s);
        }

        public void DeleteStaffMember(StaffMemberEntity sm)
        {
            _context.StaffMembers.Remove(sm);
        }

        public StaffMemberEntity GetStaffMember(int staffId)
        {
            return _context.StaffMembers.FirstOrDefault(s => s.Id == staffId);
        }

        public IEnumerable<StaffMemberEntity> GetStaffMembers()
        {
            return _context.StaffMembers.OrderBy(s => s.FirstName);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool StaffMemberExists(int staffId)
        {
            return _context.StaffMembers.Any(s => s.Id == staffId);
        }

        public bool JobExists(int jobId)
        {
            return _context.Jobs.Any(j => j.Id == jobId);
        }

        public void SetStaffMemberMainJob(StaffMemberEntity s, JobEntity j)
        {
            if (_context.MainJobs.Any(x => x.StaffMemberId == s.Id))
            {
                MainJobEntity mainJob = _context.MainJobs.Where(m => m.StaffMemberId == s.Id).FirstOrDefault();
                mainJob.Job = j;
            }
            else
            {
                MainJobEntity mainJob = new MainJobEntity()
                {
                    StaffMember = s,
                    Job = j
                };

                _context.MainJobs.Add(mainJob);
            }
        }
    }
}
