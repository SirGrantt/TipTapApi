using Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.RepositoryInterfaces
{
    public interface IServerTeamRepository : IRepository
    {
        void AddServerTeam(ServerTeamEntity serverTeam);
        void AddCheckoutToServerTeam(ServerTeamCheckoutEntity teamCheckout);
        bool ServerTeamExists(int serverTeamId);
        ServerTeamEntity GetServerTeamById(int serverTeamId);
        bool CheckoutHasAlreadyBeenAdded(int checkoutId, DateTime shiftDate);
        void AddTipOut(TipOutEntity tipOut);
        void DeleteTipOut(int serverTeamId);
        TipOutEntity GetServerTeamTipOut(int serverTeamId);
        List<StaffMemberEntity> GetServerTeamMembers(int serverTeamId);
        void DeleteServerTeamCheckout(ServerTeamEntity team);
        IEnumerable<ServerTeamEntity> GetServerTeamsForShift(DateTime shiftDate, string lunchOrDinner);
    }
}
