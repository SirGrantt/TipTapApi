using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Common.DTOs.CheckOutDtos;
using Common.DTOs.CheckoutsRanDtos;
using Common.DTOs.EarningsDtos;
using Common.DTOs.StaffMemberDtos;
using Common.DTOs.TeamDtos;
using Common.DTOs.TipOutDtos;
using Common.Entities;
using Common.RepositoryInterfaces;
using Common.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TipTapApi.Controllers
{
    [Route("server-teams")]
    public class ServerTeamController : Controller
    {
        private ILogger<ServerTeamController> _logger;
        private CheckoutsCore checkoutsCore;
        private ServerTeamsCore serverTeamsCore;
        private EarningsCore earningsCore;
        public ServerTeamController(IServerTeamRepository teamRepo, ICheckoutRepository checkoutRepo, IEarningsRepository eRepo, ILogger<ServerTeamController> logger)
        {
            checkoutsCore = new CheckoutsCore(checkoutRepo);
            serverTeamsCore = new ServerTeamsCore(teamRepo);
            earningsCore = new EarningsCore(eRepo);
            _logger = logger;
        }

        [HttpPost("create", Name = "CreateServerTeam")]
        public IActionResult AddServerTeam([FromBody] CreateServerTeamDto data)
        {
            data.ShiftDate = Convert.ToDateTime(data.UnformattedDate);
            if (UtilityMethods.ValidateLunchOrDinnerSpecification(data.LunchOrDinner))
            {
                try
                {
                    ServerTeamDto team = serverTeamsCore.AddServerTeam(data);
                    return CreatedAtRoute("CreateServerTeam", team);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    ModelState.AddModelError("Create Server Team:", e.Message);
                    return StatusCode(500, ModelState);
                }
            }
            else
            {
                ModelState.AddModelError("Lunch or Dinner: ", "The paramater provided for lunch or dinner must be valid.");
                return BadRequest(ModelState);
            }
        }

        [HttpPost("add-checkout")]
        public IActionResult AddCheckoutToServerTeam([FromBody] AddCheckoutToServerTeamDto data)
        {
            try
            {
                if (!checkoutsCore.CheckoutExistsById(data.CheckoutId) || !serverTeamsCore.ServerTeamExists(data.ServerTeamId))
                {
                    ModelState.AddModelError("Checkout Not Found", "The checkout ID provided does not match any existing checkouts.");
                    return BadRequest(ModelState);
                }
                serverTeamsCore.AddCheckoutToServerTeam(data.ServerTeamId, checkoutsCore.GetCheckoutEntityById(data.CheckoutId));
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
                List<CheckoutEntity> checkouts = checkoutsCore.GetCheckoutEntitiesForAServerTeam(data.ServerTeamId).ToList();
                EarningDto earningDto = serverTeamsCore.RunServerTeamCheckout(data, checkouts);
                TipOutDto tipOutDto = serverTeamsCore.GetServerTeamTipOut(data.ServerTeamId);
                List<StaffMemberDto> teammates = serverTeamsCore.GetStaffMembersOnServerTeam(data.ServerTeamId);
                List<EarningDto> earnings = earningsCore.AddEarning(teammates, earningDto);

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

    }
}