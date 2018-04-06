using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Common;
using Common.DTOs.CheckOutDtos;
using Common.DTOs.JobDtos;
using Common.DTOs.StaffMemberDtos;
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
        private ILogger<CheckoutController> _logger;
        public CheckoutController(ICheckoutRepository coRepository, ILogger<CheckoutController> logger, IStaffMemberRepository sRepo, IJobRepository jRepo)
        {
            _checkoutsCore = new CheckoutsCore(coRepository);
            _logger = logger;
            _staffCore = new StaffMembersCore(sRepo);
            _jobCore = new JobCore(jRepo);
        }

        [HttpPost("create", Name = "CreateCheckout")]
        public IActionResult CreateCheckOut([FromBody] CreateCheckoutDto data)
        {
            //convert the date sent from the client into a DateTime format
            data.ShiftDate = Convert.ToDateTime(data.UnformattedDate);

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
    }
}