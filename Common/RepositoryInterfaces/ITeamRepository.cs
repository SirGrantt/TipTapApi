using Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.RepositoryInterfaces
{
    public interface ITeamRepository : IRepository
    {
        void AddTeam(TeamEntity team);
        void AddCheckoutToTeam(TeamCheckoutEntity teamCheckout);
        bool TeamExists(int teamId);
        TeamEntity GetTeamById(int teamId);
        bool CheckoutHasAlreadyBeenAdded(int checkoutId, DateTime shiftDate);
        void AddTipOut(TipOutEntity tipOut);
        void DeleteTipOut(int teamId);
        TipOutEntity GetTeamTipOut(int TeamId);
        List<StaffMemberEntity> GetTeamMembers(int teamId);
        void DeleteTeamCheckout(TeamEntity team);
        IEnumerable<TeamEntity> GetTeamsForShift(DateTime shiftDate, string lunchOrDinner, string teamType);
        void RemoveCheckoutFromTeam(int teamId, int checkoutId);
    }
}
