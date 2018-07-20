using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Common;
using Common.DTOs.CheckOutDtos;
using Common.DTOs.EarningsDtos;
using Common.DTOs.JobDtos;
using Common.DTOs.StaffMemberDtos;
using Common.DTOs.TeamDtos;
using Common.RepositoryInterfaces;
using Common.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TipTapApi.Controllers
{
    [Route("checkout")]
    public class CheckoutController : Controller
    {
        private CheckoutsCore _checkoutsCore;
        private StaffMembersCore _staffCore;
        private JobCore _jobCore;
        private ServerTeamsCore _serverTeamCore;
        private EarningsCore _earningsCore;
        private ILogger<CheckoutController> _logger;
        public CheckoutController(ICheckoutRepository coRepository, ILogger<CheckoutController> logger, IStaffMemberRepository sRepo, IJobRepository jRepo, ITeamRepository teamRepo, IEarningsRepository earningsRepo)
        {
            _checkoutsCore = new CheckoutsCore(coRepository);
            _logger = logger;
            _staffCore = new StaffMembersCore(sRepo);
            _jobCore = new JobCore(jRepo);
            _serverTeamCore = new ServerTeamsCore(teamRepo);
            _earningsCore = new EarningsCore(earningsRepo);
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

        //[HttpPost("create", Name = "CreateCheckout")]
        //public IActionResult CreateCheckOutTest([FromBody] CreateCheckoutDto data)
        //{
        //    //convert the date sent from the client into a DateTime format
        //    data.ShiftDate = Convert.ToDateTime(data.UnformattedDate);

        //    ControllerMethodWithTry<IActionResult>(() =>
        //    {
        //        UtilityMethods.ValidateLunchOrDinnerSpecification(data.LunchOrDinner);
        //        StaffMemberDto staffMember = _staffCore.GetStaffMember(data.StaffMemberId);
        //        JobDto job = _jobCore.GetJobByTitle(data.JobWorkedTitle);
        //        CheckoutDto checkout = _checkoutsCore.CreateCheckout(data, staffMember, job);
        //        return CreatedAtRoute("CreateCheckout", checkout);
        //    });

        //}


        [HttpPost("create", Name = "CreateCheckout")]
        public IActionResult CreateCheckOut([FromBody] CreateCheckoutDto data)
        {
            //convert the date sent from the client into a DateTime format
            data.ShiftDate = Convert.ToDateTime(data.StringDate).Date;

            try
            {
                UtilityMethods.ValidateLunchOrDinnerSpecification(data.LunchOrDinner);
                StaffMemberDto staffMember = _staffCore.GetStaffMember(data.StaffMemberId);
                JobDto job = _jobCore.GetJobByTitle(data.JobWorkedTitle);
                CheckoutDto checkout = _checkoutsCore.CreateCheckout(data, staffMember, job);
                return CreatedAtRoute("CreateCheckout", checkout);
            }
            catch (Exception e)
            {
                if (e.InnerException is InvalidOperationException)
                {
                    return BadRequest(e.Message);
                }
                _logger.LogError(e.Message);
                ModelState.AddModelError("Create Checkout Failure", e.Message);
                return StatusCode(500, ModelState);
            }

        }

        [HttpPost("get-by-date")]
        public IActionResult GetCheckoutByDate([FromBody] GetCheckoutByDateDto data)
        {
            try
            {
                StaffMemberDto staffDto = _staffCore.GetStaffMember(data.StaffMemberId);
                CheckoutDto checkout = _checkoutsCore.GetCheckoutByDate(data, staffDto);
                return Ok(checkout);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                ModelState.AddModelError("Checkout by Date Error: ", e.Message);
                if (e.InnerException is KeyNotFoundException)
                {
                    return BadRequest(ModelState);
                }

                return StatusCode(500, ModelState);
            }
        }

        [HttpPost("delete")]
        public IActionResult DeleteCheckout([FromBody] DeleteCheckoutDto data)
        {
            try
            {
                if (!_checkoutsCore.CheckoutExistsById(data.CheckoutId))
                {
                    ModelState.AddModelError("Invalid ID: ", "No Checkout with that ID was found.");
                    return BadRequest(ModelState);
                }

                _checkoutsCore.DeleteCheckout(data.CheckoutId);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                ModelState.AddModelError("Error Deleting Checkout: ", e.Message);
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost("get-all-for-shift")]
        public IActionResult GetCheckoutsForShift([FromBody] GetCheckoutsForShiftDto data)
        {
            try
            {
                data.ShiftDate = Convert.ToDateTime(data.StringDate).Date;
                UtilityMethods.ValidateLunchOrDinnerSpecification(data.LunchOrDinner);

                List<CheckoutOverviewDto> checkouts = _checkoutsCore.GetCheckoutsForShift(data.ShiftDate, data.LunchOrDinner).ToList();
                List<ServerTeamDto> serverTeams = _serverTeamCore.GetServerTeamsForShift(data.ShiftDate, data.LunchOrDinner, "Server");
                List<EarningDto> shiftEarnings = _earningsCore.GetEarningsForShift(data.ShiftDate, data.LunchOrDinner);
                CheckoutPagePresentationDto pageData = _checkoutsCore.FormatPageData(checkouts, serverTeams, shiftEarnings);
                return Ok(pageData);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Get Checkouts Error", e.Message);
                if (e is InvalidOperationException)
                {
                    return BadRequest(ModelState);
                }
                _logger.LogError(e.Message);
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost("update")]
        public IActionResult UpdateCheckout([FromBody] UpdateCheckoutDto data)
        {
            try
            {
                if (!_checkoutsCore.CheckoutExistsById(data.Id))
                {
                    ModelState.AddModelError("Not Found", "No checkout with the provided ID was found.");
                    return BadRequest(ModelState);
                }

                bool updateSucceeded = _checkoutsCore.UpdateCheckout(data);

                if (updateSucceeded)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error Updating Checkout", e.Message);
                if (e is InvalidOperationException)
                {
                    return BadRequest(ModelState);
                }
                _logger.LogError(e.InnerException.Message);
                return StatusCode(500);
            }
        }
    }
}