using AutoMapper;
using Common.DTOs.CheckoutsRanDtos;
using Common.DTOs.EarningsDtos;
using Common.DTOs.StaffMemberDtos;
using Common.DTOs.TeamDtos;
using Common.DTOs.TipOutDtos;
using Common.Entities;
using Common.RepositoryInterfaces;
using Common.Utilities;
using Domain.Checkouts;
using Domain.StaffEarnings;
using Domain.Teams;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class ServerTeamsCore
    {
        ITeamRepository teamRepository;

        public ServerTeamsCore(ITeamRepository repository)
        {
            teamRepository = repository;
        }

        public bool ServerTeamExists(int serverTeamId)
        {
            return teamRepository.TeamExists(serverTeamId);
        }

        public ServerTeamDto AddServerTeam(CreateServerTeamDto data)
        {
            TeamEntity team = new TeamEntity
            {
                ShiftDate = Convert.ToDateTime(data.StringDate),
                LunchOrDinner = data.LunchOrDinner,
                TeamType = "Server"
            };

            teamRepository.AddTeam(team);
            UtilityMethods.VerifyDatabaseSaveSuccess(teamRepository);

            ServerTeamDto teamDto = Mapper.Map<ServerTeamDto>(team);
            return teamDto;
        }

        public void AddCheckoutToTeam(int serverTeamId, CheckoutEntity checkout)
        {
            TeamEntity team = teamRepository.GetTeamById(serverTeamId);

            if (checkout.ShiftDate.Date != team.ShiftDate.Date)
            {
                throw new InvalidOperationException("You cannot add a checkout to a team with different shift dates.");
            }

            TeamCheckoutEntity addedCheckout = new TeamCheckoutEntity
            {
                Team = team,
                Checkout = checkout,
                ShiftDate = checkout.ShiftDate
            };

            if (teamRepository.CheckoutHasAlreadyBeenAdded(checkout.Id, checkout.ShiftDate))
            {
                throw new InvalidOperationException("That checkout is currently on another team");
            }

            teamRepository.AddCheckoutToTeam(addedCheckout);

            if (!teamRepository.Save())
            {
                throw new Exception("An unexpected error occured while trying to add the checkout to the team.");
            }

        }

        public EarningDto RunServerTeamCheckout(RunServerTeamCheckoutDto data, List<CheckoutEntity> checkouts)
        {
            TeamEntity teamEntity = teamRepository.GetTeamById(data.ServerTeamId);

            //Check for the team to have an existing tipout, if it does remove it to not have incorrect tipout data.
            if (teamEntity.CheckoutHasBeenRun == true)
            {
                teamRepository.DeleteTipOut(data.ServerTeamId);
                teamEntity.CheckoutHasBeenRun = false;
            }

            ServerTeam team = new ServerTeam(teamEntity.ShiftDate);
            Mapper.Map(teamEntity, team);

            foreach (CheckoutEntity c in checkouts)
            {
                Checkout x = Mapper.Map<Checkout>(c);
                team.CheckOuts.Add(x);
            }

            //The earning is returned from the method called, and a tipout property is set on the team
            Earnings earning = team.RunCheckout()[0];

            //The teams tipout is accessed here and saved to the database
            //The earning is tied to the server and not the checkout, so the earning
            //gets added and saved once this method returns an earning DTO 
            TipOutEntity tipOutEntity = Mapper.Map<TipOutEntity>(team.TipOut);
            tipOutEntity.Team = teamEntity;
            teamRepository.AddTipOut(tipOutEntity);
            teamEntity.CheckoutHasBeenRun = true;

            if (!teamRepository.Save())
            {
                throw new Exception("An unexpected error occured while saving the tipout for the team's checkout");
            }

            return Mapper.Map<EarningDto>(earning);
        } 

        public TipOutDto GetServerTeamTipOut(int serverTeamId)
        {
            if (!teamRepository.TeamExists(serverTeamId))
            {
                throw new KeyNotFoundException("No team with the provided ID was found.");
            }

            TeamEntity team = teamRepository.GetTeamById(serverTeamId);
            if (team.CheckoutHasBeenRun == false)
            {
                throw new InvalidOperationException("No tipout exists for the team because their checkout has not be run");
            }

            TipOutEntity tipOutEntity = teamRepository.GetTeamTipOut(serverTeamId);
            return Mapper.Map<TipOutDto>(tipOutEntity);
        }

        public List<StaffMemberDto> GetStaffMembersOnServerTeam(int serverTeamId)
        {
            List<StaffMemberEntity> teamMembers = teamRepository.GetTeamMembers(serverTeamId);
            List<StaffMemberDto> teamMembersDto = new List<StaffMemberDto>();

            foreach (StaffMemberEntity x in teamMembers)
            {
                teamMembersDto.Add(Mapper.Map<StaffMemberDto>(x));
            }
            return teamMembersDto;
        }

        public void DeleteServerTeamCheckout(int serverTeamId)
        {
            TeamEntity team = teamRepository.GetTeamById(serverTeamId);
            teamRepository.DeleteTeamCheckout(team);
            //Reset the checkouthasbeenrun property in order to prevent bugs when other things need to know if this checkout
            //has been run in the past.
            team.CheckoutHasBeenRun = false;

            UtilityMethods.VerifyDatabaseSaveSuccess(teamRepository);
        }

        public ServerTeamDto GetServerTeamById(int serverTeamId)
        {
            ServerTeamDto team = Mapper.Map<ServerTeamDto>(teamRepository.GetTeamById(serverTeamId));
            return team;
        }

        public List<ServerTeamDto> GetServerTeamsForShift(DateTime shiftDate, string lunchOrDinner, string teamType)
        {
            IEnumerable<TeamEntity> serverTeamEntities = teamRepository.GetTeamsForShift(shiftDate, lunchOrDinner, teamType);
            List<ServerTeamDto> serverTeams = new List<ServerTeamDto>();

            foreach (TeamEntity s in serverTeamEntities)
            {
                serverTeams.Add(Mapper.Map<ServerTeamDto>(s));
            }

            return serverTeams;
        }


        public void RemoveCheckoutFromServerTeam(int serverTeamId, int checkoutId)
        {
            teamRepository.RemoveCheckoutFromTeam(serverTeamId, checkoutId);

            UtilityMethods.VerifyDatabaseSaveSuccess(teamRepository);
        }
        
    }
}
