using Domain.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Groups
{
    public class ServerGroup
    {
        public int Id { get; set; }
        public List<ServerTeam> ServerTeams { get; set; }
        public decimal ServersTipOutToBar { get; set; }
        public decimal ServersTipOutToSAs { get; set; }

        public ServerGroup()
        {
            ServerTeams = new List<ServerTeam>();
        }

        public void AddServerTeam(ServerTeam serverTeam)
        {
            if (ServerTeams.Any(st => st.Id == serverTeam.Id))
            {
                return;
            }

            ServerTeams.Add(serverTeam);
        }

        public void RunServerCheckOut(int teamId)
        {
            //Figure out the best way to find the correct server team to call the checkoutmethod on
            ServerTeam teamToCheckOut = ServerTeams.FirstOrDefault(t => t.Id == teamId);
            
            if (teamToCheckOut == null)
            {
                throw new KeyNotFoundException("No team with that ID could be found");
            }

            if (teamToCheckOut.CheckoutHasBeenRun == true)
            {
                ServersTipOutToBar = ServersTipOutToBar - teamToCheckOut.BarTipOut;
                ServersTipOutToSAs = ServersTipOutToSAs = teamToCheckOut.SATipOut;
            }

            teamToCheckOut.RunCheckout();
            ServersTipOutToSAs += teamToCheckOut.SATipOut;
            ServersTipOutToBar += teamToCheckOut.BarTipOut;
        }
    }
}
