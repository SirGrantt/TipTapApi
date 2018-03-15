using Domain.Groups;
using Domain.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Teams
{
    public class ServerTeamEditor : ITeamEditor<ServerGroup, ServerTeam, Server>
    {
        public void AddTeamMember(ServerTeam serverTeam, Server server)
        {
            if (serverTeam.Servers.Any(s => s.Employee.Id == server.Employee.Id))
            {
                throw new InvalidOperationException("Cannot add a server to a team that has the same employee assigned.");
            }

            serverTeam.Servers.Add(server);
        }

        public void RemoveTeamMember(ServerGroup serverGroup, int serverTeamId, int serverId)
        {
            if (!serverGroup.ServerTeams.Any(t => t.Id == serverTeamId))
            {
                throw new KeyNotFoundException("No server team with the provided ID exists.");
            }

            ServerTeam team = serverGroup.ServerTeams.First(t => t.Id == serverTeamId);

            if (!team.Servers.Any(s => s.Id == serverId))
            {
                throw new KeyNotFoundException("No server with the provided ID exists in the team with the provided team ID.");
            }

            if (team.CheckoutHasBeenRun == true)
            {
                serverGroup.ServersTipOutToBar = serverGroup.ServersTipOutToBar - team.BarTipOut;
                serverGroup.ServersTipOutToSAs = serverGroup.ServersTipOutToSAs - team.SATipOut;
                team.ResetTipOuts();
            }

            Server serverToRemove = team.Servers.First(s => s.Id == serverId);
            team.Servers.Remove(serverToRemove);
        }
    }
}
