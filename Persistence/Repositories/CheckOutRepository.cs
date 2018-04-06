﻿using Common.Entities;
using Common.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence.Repositories
{
    public class CheckoutRepository : ICheckoutRepository
    {
        private CheckoutManagerContext _context;

        public CheckoutRepository(CheckoutManagerContext context)
        {
            _context = context;
        }

        public void AddCheckOut(CheckoutEntity c)
        {

            _context.CheckOuts.Add(c);
        }

        public bool CheckoutExists(DateTime date, int staffId, string lunchOrDinner)
        {
            return _context.CheckOuts.Any(c => c.ShiftDate == date && c.StaffMemberId == staffId && c.LunchOrDinner == lunchOrDinner.ToLower());
        }

        public bool CheckoutExistsById(int checkoutId)
        {
            return _context.CheckOuts.Any(c => c.Id == checkoutId);
        }

        public void DeleteCheckOut(CheckoutEntity c)
        {
            _context.CheckOuts.Remove(c);
        }

        public CheckoutEntity GetCheckOutById(int checkOutId)
        {
            return _context.CheckOuts.FirstOrDefault(c => c.Id == checkOutId);
        }

        public CheckoutEntity GetCheckOutForStaffMemberForSpecificDate(DateTime date, int staffMemberId, string lunchOrDinner)
        {
            return _context.CheckOuts
                .Where(c => c.ShiftDate == date && c.StaffMemberId == staffMemberId && c.LunchOrDinner == lunchOrDinner.ToLower())
                .Include(c => c.Job)
                .FirstOrDefault();
        }

        public IEnumerable<CheckoutEntity> GetCheckOutsForADate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CheckoutEntity> GetCheckoutsForAServerTeam(int serverTeamId)
        {
            List<CheckoutEntity> checkouts = new List<CheckoutEntity>();
            IEnumerable<ServerTeamCheckoutEntity> reference = from r in _context.ServerTeamCheckouts
                                                       where r.TeamId == serverTeamId
                                                       select r;
            foreach (ServerTeamCheckoutEntity s in reference)
            {
                var checkout = _context.CheckOuts.Where(p => p.Id == s.CheckoutId)
                    .Include(p => p.Job).FirstOrDefault();
                checkouts.Add(checkout);
            }

            return checkouts;
        }

        public IEnumerable<CheckoutEntity> GetCheckOutsForAStaffMember(StaffMemberEntity staffmember)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CheckoutEntity> GetCheckOutsForAStaffMemberForAJob(StaffMemberEntity staffMember, JobEntity j)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}