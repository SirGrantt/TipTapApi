﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using AutoMapper;
using Common.DTOs.CheckOutDtos;
using Common.DTOs.CheckoutsRanDtos;
using Common.DTOs.EarningsDtos;
using Common.DTOs.StaffMemberDtos;
using Common.DTOs.TeamDtos;
using Common.DTOs.TipOutDtos;
using Common.Entities;
using Common.RepositoryInterfaces;
using Common.Utilities;
using Domain.StaffEarnings;
using Domain.Teams;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TipTapApi.Controllers
{
    [Route("team")]
    public class TeamController : Controller
    {
        private ILogger<TeamController> _logger;
        private CheckoutsCore checkoutsCore;
        private ServerTeamsCore serverTeamsCore;
        private EarningsCore earningsCore;
        private BarCore _barCore;
        public TeamController(ITeamRepository teamRepo, ICheckoutRepository checkoutRepo, IEarningsRepository eRepo, ILogger<TeamController> logger)
        {
            checkoutsCore = new CheckoutsCore(checkoutRepo);
            serverTeamsCore = new ServerTeamsCore(teamRepo);
            earningsCore = new EarningsCore(eRepo);
            _barCore = new BarCore(teamRepo, checkoutRepo);
            _logger = logger;
        }

        //public TResult ControllerMethodWithTry<TResult>(Func<TResult> method) where TResult:ObjectResult
        //{
        //    try
        //    {
        //        return method();
        //    }
        //    catch (Exception e)
        //    {
        //        if (e.InnerException is InvalidOperationException)
        //        {
        //            return BadRequest(e.Message);
        //        }
        //        _logger.LogError(e.Message);
        //        ModelState.AddModelError("Create Checkout Failure", e.Message);
        //        return StatusCode(500, ModelState);
        //    }
        //}

        [HttpPost("create", Name = "CreateServerTeam")]
        public IActionResult AddServerTeam([FromBody] CreateServerTeamDto data)
        {
            try
            {
                data.ShiftDate = Convert.ToDateTime(data.StringDate);
                UtilityMethods.ValidateLunchOrDinnerSpecification(data.LunchOrDinner);

                ServerTeamDto team = serverTeamsCore.AddServerTeam(data);
                return CreatedAtRoute("CreateServerTeam", team);
            }
            catch (Exception e)
            {
                if (e.InnerException is InvalidOperationException)
                {
                    return BadRequest(e.Message);
                }
                _logger.LogError(e.Message);
                ModelState.AddModelError("Create Server Team:", e.Message);
                return StatusCode(500, ModelState);
            }

        }

        [HttpPost("add-checkout")]
        public IActionResult AddCheckoutToTeam([FromBody] AddCheckoutToTeamDto data)
        {
            try
            {
                if (!checkoutsCore.CheckoutExistsById(data.CheckoutId) || !serverTeamsCore.ServerTeamExists(data.ServerTeamId))
                {
                    ModelState.AddModelError("Checkout Not Found", "The checkout ID provided does not match any existing checkouts.");
                    return BadRequest(ModelState);
                }
                serverTeamsCore.AddCheckoutToTeam(data.ServerTeamId, checkoutsCore.GetCheckoutEntityById(data.CheckoutId));
                return Ok();
            }
            catch(Exception e)
            {
                if (e.InnerException is InvalidOperationException)
                {
                    return BadRequest(e.InnerException);
                }
                ModelState.AddModelError("Add Checkout to Team Failure", e.Message);
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost("run-barteam-checkout", Name = "RunBarCheckout")]
        public IActionResult RunBarTeamCheckout([FromBody] RunBarTeamCheckoutData data)
        {
            try
            {
                data.ShiftDate = Convert.ToDateTime(data.ShiftDate);
                UtilityMethods.ValidateLunchOrDinnerSpecification(data.LunchOrDinner);
                if (data.BarBackCount < 1)
                {
                    ModelState.AddModelError("BarBack Count Error", "BarBack count cannot be 0");
                    return BadRequest(ModelState);
                }
                List<Earnings> earnings = _barCore.RunBarTeamCheckout(data.ShiftDate, data.LunchOrDinner, data.BarBackCount);
                List<Earnings> addedEarnings = earningsCore.AddNonServerEarnings(earnings);
                
                // The staff member earnings have to be set to null to prevent circular data being passed down
                foreach (Earnings e in addedEarnings)
                {
                    e.StaffMember.Earnings = null;
                }
                return Created("RunBarCheckout", addedEarnings);
                //return CreatedAtRoute("RunBarCheckout", groupedEarnings);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("run-checkout", Name = "RunCheckout")]
        public IActionResult RunServerTeamCheckout([FromBody] RunServerTeamCheckoutDto data)
        {
            try
            {
                if (!serverTeamsCore.ServerTeamExists(data.ServerTeamId))
                {
                    ModelState.AddModelError("Team ID Not Found", "No team with that ID was found");
                    return BadRequest(ModelState);
                }

                UtilityMethods.ValidateLunchOrDinnerSpecification(data.LunchOrDinner);
                data.FormattedDate = Convert.ToDateTime(data.StringDate);

                ServerTeamDto team = serverTeamsCore.GetServerTeamById(data.ServerTeamId);
                List<StaffMemberDto> teammates = serverTeamsCore.GetStaffMembersOnServerTeam(data.ServerTeamId);

                //need to reset earnings for the staff memebers if the checkout is being updated
                earningsCore.ResetEarningsForServerTeam(teammates, data.FormattedDate, data.LunchOrDinner);


                List<CheckoutEntity> checkouts = checkoutsCore.GetCheckoutEntitiesForATeam(data.ServerTeamId).ToList();
                EarningDto earningDto = serverTeamsCore.RunServerTeamCheckout(data, checkouts);
                TipOutDto tipOutDto = serverTeamsCore.GetServerTeamTipOut(data.ServerTeamId);

                //The earnings get added to servers here to seperate earnings work into its own section
                List<EarningDto> earnings = earningsCore.AddServerEarning(teammates, earningDto);

                ServerTipOutEarningDto tipoutAndEarning = new ServerTipOutEarningDto
                {
                    TeamTipOut = tipOutDto,
                    ServerEarnings = earnings
                };

                return CreatedAtRoute("RunCheckout", tipoutAndEarning);
            }
            catch(Exception e)
            {
                if (e.InnerException is InvalidOperationException)
                {
                    ModelState.AddModelError("Invalid Operation", e.Message);
                    return BadRequest(ModelState);
                }
                else if (e.InnerException is KeyNotFoundException)
                {
                    ModelState.AddModelError("Key Not Found", e.Message);
                    return BadRequest(ModelState);
                }
                ModelState.AddModelError("Run CheckoutError", e.Message);
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost("reset-checkout")]
        public IActionResult ResetCheckout([FromBody] ResetTeamCheckoutDto data)
        {
            try
            {
                if (!serverTeamsCore.ServerTeamExists(data.ServerTeamId))
                {
                    return BadRequest("No Server Team with that Id exists");
                }

                UtilityMethods.ValidateLunchOrDinnerSpecification(data.LunchOrDinner);

                ServerTeamDto team = serverTeamsCore.GetServerTeamById(data.ServerTeamId);
                
                if (team.CheckoutHasBeenRun == false)
                {
                    return NoContent();
                }

                data.ShiftDate = Convert.ToDateTime(data.StringDate);
                List<StaffMemberDto> teammates = serverTeamsCore.GetStaffMembersOnServerTeam(data.ServerTeamId);

                serverTeamsCore.DeleteServerTeamCheckout(data.ServerTeamId);
                earningsCore.ResetEarningsForServerTeam(teammates, data.ShiftDate, data.LunchOrDinner);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                ModelState.AddModelError("Reset Checkout Error", e.Message);
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost("remove-checkout-from-server-team")]
        public IActionResult RemoveCheckoutFromServerTeam([FromBody] RemoveCheckoutFromServerTeamDto data)
        {
            try
            {
                data.ShiftDate = Convert.ToDateTime(data.StringDate);
                UtilityMethods.ValidateLunchOrDinnerSpecification(data.LunchOrDinner);

                if (!serverTeamsCore.ServerTeamExists(data.ServerTeamId))
                {
                    return BadRequest("No team with that ID was found");
                }

                if (!checkoutsCore.CheckoutExistsById(data.CheckoutId))
                {
                    return BadRequest("No team with that ID was found");
                }

                List<CheckoutDto> checkoutsForTeam = Mapper.Map<List<CheckoutDto>>(checkoutsCore.GetCheckoutEntitiesForATeam(data.ServerTeamId));

                if (!checkoutsForTeam.Any(c => c.Id == data.CheckoutId))
                {
                    return BadRequest("The provided checkout ID was not found on the provided server team");
                }

                List<StaffMemberDto> teammates = serverTeamsCore.GetStaffMembersOnServerTeam(data.ServerTeamId);
                serverTeamsCore.DeleteServerTeamCheckout(data.ServerTeamId);
                earningsCore.ResetEarningsForServerTeam(teammates, data.ShiftDate, data.LunchOrDinner);
                serverTeamsCore.RemoveCheckoutFromServerTeam(data.ServerTeamId, data.CheckoutId);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                ModelState.AddModelError("Remove Checkout from Server Team Error", e.Message);
                return StatusCode(500, ModelState);
            }
        }

    }
}