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
            if (serverTeam.Servers.Any(s => s.StaffMember.Id == server.StaffMember.Id))
            {
                throw new InvalidOperationException("Cannot add a server to a team that has the same employee assigned.");
            }

            serverTeam.Servers.Add(server);
        }

        public void RemoveTeamMember(ServerGroup group, int teamId, int serverId)
        {
            ServerTeam team = GetTeam(group, teamId);
            Server serverToRemove = GetTeamMember(team, serverId);

            if (team.CheckoutHasBeenRun == true)
            {
                group.ServersTipOutToBar = group.ServersTipOutToBar - team.BarTipOut;
                group.ServersTipOutToSAs = group.ServersTipOutToSAs - team.SATipOut;
                team.CheckoutHasBeenRun = false; 
            }

            team.Servers.Remove(serverToRemove);
        }

        public void UpdateTeamMember(ServerGroup group, int teamId, Server teamMember)
        {
            ServerTeam team = GetTeam(group, teamId);
            Server server = GetTeamMember(team, teamMember.Id);

            server = teamMember;
        }

        public ServerTeam GetTeam(ServerGroup group, int teamId)
        {
            if (!group.ServerTeams.Any(t => t.Id == teamId))
            {
                throw new KeyNotFoundException("No server team with the provided ID exists.");
            }

            ServerTeam team = group.ServerTeams.First(t => t.Id == teamId);
            return team;
        }

        public Server GetTeamMember(ServerTeam team, int teamMemberId)
        {
            if (!team.Servers.Any(s => s.Id == teamMemberId))
            {
                throw new KeyNotFoundException("No server with the provided ID exists in the team with the provided team ID.");
            }

            Server server = team.Servers.First(s => s.Id == teamMemberId);
            return server;
        }
    }
}
