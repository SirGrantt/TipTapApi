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
    public class ServerTeamRepository : IServerTeamRepository
    {
        private CheckoutManagerContext _context;

        public ServerTeamRepository(CheckoutManagerContext context)
        {
            _context = context;
        }

        public void AddServerTeam(ServerTeamEntity serverTeam)
        {
            _context.ServerTeams.Add(serverTeam);
        }

        public void AddCheckoutToServerTeam(ServerTeamCheckoutEntity serverTeamCheckout)
        {
            _context.ServerTeamCheckouts.Add(serverTeamCheckout);
        }

        public ServerTeamEntity GetServerTeamById(int serverTeamId)
        {
            return _context.ServerTeams.FirstOrDefault(t => t.Id == serverTeamId);
        }

        public IEnumerable<ServerTeamEntity> GetServerTeamsForShift(DateTime shiftDate, string lunchOrDinner)
        {
            List<ServerTeamEntity> teams = new List<ServerTeamEntity>();

            foreach (ServerTeamEntity x in _context.ServerTeams
                .Where(t => t.ShiftDate == shiftDate
                && t.LunchOrDinner == lunchOrDinner.ToLower()))
            {
                teams.Add(x);
            }

            return teams;
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool ServerTeamExists(int serverTeamId)
        {
            return _context.ServerTeams.Any(t => t.Id == serverTeamId);
        }

        public bool CheckoutHasAlreadyBeenAdded(int checkoutId, DateTime shiftDate)
        {
            return _context.ServerTeamCheckouts.Any(x => x.CheckoutId == checkoutId && x.ShiftDate == shiftDate);
        }

        public void AddTipOut(TipOutEntity tipOut)
        {
            _context.TipOuts.Add(tipOut);
        }

        public void DeleteTipOut(int serverTeamId)
        {
            var tipOutToRemove = _context.TipOuts.Where(t => t.ServerTeamId == serverTeamId).FirstOrDefault();
            _context.Remove(tipOutToRemove);
        }

        public TipOutEntity GetServerTeamTipOut(int serverTeamId)
        {
            return _context.TipOuts.FirstOrDefault(t => t.ServerTeamId == serverTeamId);
        }

        public List<StaffMemberEntity> GetServerTeamMembers(int serverTeamId)
        {
            List<StaffMemberEntity> staffMembers = new List<StaffMemberEntity>();
            foreach (ServerTeamCheckoutEntity x in _context.ServerTeamCheckouts
                .Where(r => r.TeamId == serverTeamId))
            {
                CheckoutEntity checkoutEntity = _context.CheckOuts.Where(c => c.Id == x.CheckoutId)
                    .Include(c => c.StaffMember).FirstOrDefault();
                StaffMemberEntity s = checkoutEntity.StaffMember;
                staffMembers.Add(s);
            }

            return staffMembers;
        }

        public void DeleteServerTeamCheckout(ServerTeamEntity team)
        {
            TipOutEntity tipOutEntity = _context.TipOuts.Where(t => t.ServerTeamId == team.Id).FirstOrDefault();
            _context.TipOuts.Remove(tipOutEntity);
        }

        public void RemoveCheckoutFromServerTeam(int serverTeamId, int checkoutId)
        {
            ServerTeamCheckoutEntity teamCheckoutReference = _context.ServerTeamCheckouts.Where(t => t.CheckoutId == checkoutId && t.TeamId == serverTeamId).FirstOrDefault();
            _context.ServerTeamCheckouts.Remove(teamCheckoutReference);
        }
    }
}
