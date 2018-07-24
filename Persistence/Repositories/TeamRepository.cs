using Common.Entities;
using Common.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private CheckoutManagerContext _context;

        public TeamRepository(CheckoutManagerContext context)
        {
            _context = context;
        }

        public void AddTeam(TeamEntity team)
        {
            _context.Teams.Add(team);
        }

        public void AddCheckoutToTeam(TeamCheckoutEntity teamCheckout)
        {
            _context.TeamCheckouts.Add(teamCheckout);
        }

        public TeamEntity GetTeamById(int teamId)
        {
            return _context.Teams.FirstOrDefault(t => t.Id == teamId);
        }

        public IEnumerable<TeamEntity> GetTeamsForShift(DateTime shiftDate, string lunchOrDinner, string teamType)
        {
            List<TeamEntity> teams = new List<TeamEntity>();

            foreach (TeamEntity x in _context.Teams
                .Where(t => t.ShiftDate.Date == shiftDate.Date
                && t.LunchOrDinner == lunchOrDinner.ToLower() && t.TeamType == teamType))
            {
                teams.Add(x);
            }

            return teams;
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool TeamExists(int teamId)
        {
            return _context.Teams.Any(t => t.Id == teamId);
        }

        public bool CheckoutHasAlreadyBeenAdded(int checkoutId, DateTime shiftDate)
        {
            return _context.TeamCheckouts.Any(x => x.CheckoutId == checkoutId && x.ShiftDate == shiftDate);
        }

        public void AddTipOut(TipOutEntity tipOut)
        {
            _context.TipOuts.Add(tipOut);
        }

        public void DeleteTipOut(int teamId)
        {
            var tipOutToRemove = _context.TipOuts.Where(t => t.ServerTeamId == teamId).FirstOrDefault();
            _context.Remove(tipOutToRemove);
        }

        public TipOutEntity GetTeamTipOut(int teamId)
        {
            return _context.TipOuts.FirstOrDefault(t => t.ServerTeamId == teamId);
        }

        public List<StaffMemberEntity> GetTeamMembers(int teamId)
        {
            List<StaffMemberEntity> staffMembers = new List<StaffMemberEntity>();
            foreach (TeamCheckoutEntity x in _context.TeamCheckouts
                .Where(r => r.TeamId == teamId))
            {
                CheckoutEntity checkoutEntity = _context.CheckOuts.Where(c => c.Id == x.CheckoutId)
                    .Include(c => c.StaffMember).FirstOrDefault();
                StaffMemberEntity s = checkoutEntity.StaffMember;
                staffMembers.Add(s);
            }

            return staffMembers;
        }

        public void DeleteTeamCheckout(TeamEntity team)
        {
            TipOutEntity tipOutEntity = _context.TipOuts.Where(t => t.ServerTeamId == team.Id).FirstOrDefault();
            if (tipOutEntity == null)
            {
                return;
            }
            else
            _context.TipOuts.Remove(tipOutEntity);
        }

        public void RemoveCheckoutFromTeam(int teamId, int checkoutId)
        {
            TeamCheckoutEntity teamCheckoutReference = _context.TeamCheckouts.Where(t => t.CheckoutId == checkoutId && t.TeamId == teamId).FirstOrDefault();
            _context.TeamCheckouts.Remove(teamCheckoutReference);
        }

        public bool BarTeamExistsForShift(DateTime shiftDate, string lunchOrDinner)
        {
            return _context.Teams.Any(t => t.ShiftDate.Date == shiftDate.Date && t.LunchOrDinner == lunchOrDinner
            && t.TeamType == "Bar");
        }

        public TeamEntity GetBarTeamForShift(DateTime shiftDate, string lunchOrDinner)
        {
            TeamEntity barTeam;
            if (BarTeamExistsForShift(shiftDate, lunchOrDinner))
            {
                barTeam = _context.Teams.FirstOrDefault(t => t.ShiftDate.Date == shiftDate.Date &&
                t.LunchOrDinner == lunchOrDinner && t.TeamType == "Bar");
                return barTeam;
            }
            else
            {
                barTeam = new TeamEntity()
                {
                    ShiftDate = shiftDate,
                    LunchOrDinner = lunchOrDinner,
                    TeamType = "Bar",
                    CheckoutHasBeenRun = false
                };
                AddTeam(barTeam);
                _context.SaveChanges();
                return barTeam;
            }
        }
    }
}
