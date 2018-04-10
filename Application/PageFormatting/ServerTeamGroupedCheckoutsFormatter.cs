using AutoMapper;
using Common.DTOs.CheckOutDtos;
using Common.DTOs.EarningsDtos;
using Common.DTOs.TeamDtos;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.PageFormatting
{
    public class ServerTeamGroupedCheckoutsFormatter
    {
        public List<TeamGroupedCheckoutsDto> FormatServerTeamGroupCheckouts(ServerTeamDto s, IEnumerable<CheckoutEntity> entities, List<CheckoutOverviewDto> checkouts, List<ServerTeamDto> serverTeams, List<EarningDto> shiftEarnings)
        {
            List<TeamGroupedCheckoutsDto> pageData = new List<TeamGroupedCheckoutsDto>();

            //Grab the first entity to get a related earning for the team
            CheckoutEntity checkout = entities.ElementAt(0);

            if (entities.Count() == 1)
            {
                //Mark the teams that have only one server so they can be presented as a solo team
                TeamGroupedCheckoutsDto soloTeam = new TeamGroupedCheckoutsDto
                {
                    CheckoutHasBeenRun = s.CheckoutHasBeenRun,
                    IsSoloTeam = true,
                    TeamId = s.Id,
                    TeamEarning = shiftEarnings.FirstOrDefault(e => e.StaffMemberId == checkout.StaffMemberId),
                };
                soloTeam.TeamCheckouts.Add(Mapper.Map<CheckoutOverviewDto>(checkout));
                pageData.Add(soloTeam);
            }
            else
            {
                TeamGroupedCheckoutsDto team = new TeamGroupedCheckoutsDto
                {
                    CheckoutHasBeenRun = s.CheckoutHasBeenRun,
                    IsSoloTeam = false,
                    TeamId = s.Id,
                    TeamEarning = shiftEarnings.FirstOrDefault(e => e.StaffMemberId == checkout.StaffMemberId)
                };

                foreach (CheckoutEntity c in entities)
                {
                    team.TeamCheckouts.Add(Mapper.Map<CheckoutOverviewDto>(c));
                }
                pageData.Add(team);
            }

            return pageData;
        }

        public List<CheckoutOverviewDto> FormatUnrunServerCheckouts(List<CheckoutOverviewDto> checkouts, List<TeamGroupedCheckoutsDto> groupedCheckouts)
        {
            List<CheckoutOverviewDto> unrunCheckouts = new List<CheckoutOverviewDto>();

            //Seperate the grouped checkouts to perform query against
            List<CheckoutOverviewDto> allGroupedCheckoutsListed = new List<CheckoutOverviewDto>();
            foreach (TeamGroupedCheckoutsDto g in groupedCheckouts)
            {
                foreach (CheckoutOverviewDto x in g.TeamCheckouts)
                {
                    allGroupedCheckoutsListed.Add(x);
                }
            }

            //Determing if each checkout has been grouped and add the ungrouped together
            foreach (CheckoutOverviewDto c in checkouts)
            {                
                if (!allGroupedCheckoutsListed.Any(x => x.Id == c.Id))
                {
                    unrunCheckouts.Add(c);
                }
            }

            return unrunCheckouts;
        }
    }
}
