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
        IServerTeamRepository serverTeamRepository;

        public ServerTeamsCore(IServerTeamRepository repository)
        {
            serverTeamRepository = repository;
        }

        public bool ServerTeamExists(int serverTeamId)
        {
            return serverTeamRepository.ServerTeamExists(serverTeamId);
        }

        public ServerTeamDto AddServerTeam(CreateServerTeamDto data)
        {
            ServerTeamEntity team = new ServerTeamEntity
            {
                ShiftDate = Convert.ToDateTime(data.StringDate),
                LunchOrDinner = data.LunchOrDinner,
            };

            serverTeamRepository.AddServerTeam(team);
            UtilityMethods.VerifyDatabaseSaveSuccess(serverTeamRepository);

            ServerTeamDto teamDto = Mapper.Map<ServerTeamDto>(team);
            return teamDto;
        }

        public void AddCheckoutToServerTeam(int serverTeamId, CheckoutEntity checkout)
        {
            ServerTeamEntity team = serverTeamRepository.GetServerTeamById(serverTeamId);

            if (checkout.ShiftDate.Date != team.ShiftDate.Date)
            {
                throw new InvalidOperationException("You cannot add a checkout to a team with different shift dates.");
            }

            ServerTeamCheckoutEntity addedCheckout = new ServerTeamCheckoutEntity
            {
                ServerTeam = team,
                Checkout = checkout,
                ShiftDate = checkout.ShiftDate
            };

            if (serverTeamRepository.CheckoutHasAlreadyBeenAdded(checkout.Id, checkout.ShiftDate))
            {
                throw new InvalidOperationException("That checkout is currently on another team");
            }

            serverTeamRepository.AddCheckoutToServerTeam(addedCheckout);

            if (!serverTeamRepository.Save())
            {
                throw new Exception("An unexpected error occured while trying to add the checkout to the team.");
            }

        }

        public EarningDto RunServerTeamCheckout(RunServerTeamCheckoutDto data, List<CheckoutEntity> checkouts)
        {
            ServerTeamEntity teamEntity = serverTeamRepository.GetServerTeamById(data.ServerTeamId);

            //Check for the team to have an existing tipout, if it does remove it to not have incorrect tipout data.
            if (teamEntity.CheckoutHasBeenRun == true)
            {
                serverTeamRepository.DeleteTipOut(data.ServerTeamId);
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
            Earnings earning = team.RunCheckout();

            //The teams tipout is accessed here and saved to the database
            //The earning is tied to the server and not the checkout, so the earning
            //gets added and saved once this method returns an earning DTO 
            TipOutEntity tipOutEntity = Mapper.Map<TipOutEntity>(team.TipOut);
            tipOutEntity.ServerTeam = teamEntity;
            serverTeamRepository.AddTipOut(tipOutEntity);
            teamEntity.CheckoutHasBeenRun = true;

            if (!serverTeamRepository.Save())
            {
                throw new Exception("An unexpected error occured while saving the tipout for the team's checkout");
            }

            return Mapper.Map<EarningDto>(earning);
        } 

        public TipOutDto GetServerTeamTipOut(int serverTeamId)
        {
            if (!serverTeamRepository.ServerTeamExists(serverTeamId))
            {
                throw new KeyNotFoundException("No team with the provided ID was found.");
            }

            ServerTeamEntity team = serverTeamRepository.GetServerTeamById(serverTeamId);
            if (team.CheckoutHasBeenRun == false)
            {
                throw new InvalidOperationException("No tipout exists for the team because their checkout has not be run");
            }

            TipOutEntity tipOutEntity = serverTeamRepository.GetServerTeamTipOut(serverTeamId);
            return Mapper.Map<TipOutDto>(tipOutEntity);
        }

        public List<StaffMemberDto> GetStaffMembersOnServerTeam(int serverTeamId)
        {
            List<StaffMemberEntity> teamMembers = serverTeamRepository.GetServerTeamMembers(serverTeamId);
            List<StaffMemberDto> teamMembersDto = new List<StaffMemberDto>();

            foreach (StaffMemberEntity x in teamMembers)
            {
                teamMembersDto.Add(Mapper.Map<StaffMemberDto>(x));
            }
            return teamMembersDto;
        }

        public void DeleteServerTeamCheckout(int serverTeamId)
        {
            ServerTeamEntity team = serverTeamRepository.GetServerTeamById(serverTeamId);
            serverTeamRepository.DeleteServerTeamCheckout(team);
            //Reset the checkouthasbeenrun property in order to prevent bugs when other things need to know if this checkout
            //has been run in the past.
            team.CheckoutHasBeenRun = false;

            UtilityMethods.VerifyDatabaseSaveSuccess(serverTeamRepository);
        }

        public ServerTeamDto GetServerTeamById(int serverTeamId)
        {
            ServerTeamDto team = Mapper.Map<ServerTeamDto>(serverTeamRepository.GetServerTeamById(serverTeamId));
            return team;
        }

        public List<ServerTeamDto> GetServerTeamsForShift(DateTime shiftDate, string lunchOrDinner)
        {
            IEnumerable<ServerTeamEntity> serverTeamEntities = serverTeamRepository.GetServerTeamsForShift(shiftDate, lunchOrDinner);
            List<ServerTeamDto> serverTeams = new List<ServerTeamDto>();

            foreach (ServerTeamEntity s in serverTeamEntities)
            {
                serverTeams.Add(Mapper.Map<ServerTeamDto>(s));
            }

            return serverTeams;
        }


        public void RemoveCheckoutFromServerTeam(int serverTeamId, int checkoutId)
        {
            serverTeamRepository.RemoveCheckoutFromServerTeam(serverTeamId, checkoutId);

            UtilityMethods.VerifyDatabaseSaveSuccess(serverTeamRepository);
        }

    }
}
