using Application;
using Common.DTOs.StaffMemberDtos;
using AutoMapper;
using Common;
using FluentValidation.Results;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTOs.JobDtos;
using Domain.StaffMembers;
using Common.RepositoryInterfaces;
using Common.Entities;
using Domain.Jobs;

namespace TipTapApi.Controllers
{
    [Route("staff")]
    public class StaffController : Controller
    {
        IStaffMemberRepository _repo;
        private ILogger<StaffController> _logger;
        private StaffMembersCore _staffCore;
        private JobCore _jobCore;
        private string errorMsg = "An error occured while processing your request";
        public StaffController(IStaffMemberRepository repo, IJobRepository jobRepository, ILogger<StaffController> logger)
        {
            _repo = repo;
            _logger = logger;
            _staffCore = new StaffMembersCore(_repo);
            _jobCore = new JobCore(jobRepository);
        }

        [HttpGet()]
        public IActionResult GetAllStaff()
        {
            try
            {
                var staff = _staffCore.GetStaffMembers();

                if (staff.Count() == 0)
                {
                    return NotFound();
                }

                return Ok(staff);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while loading the staff Members");
                return StatusCode(500, errorMsg);
            }

        }

        [HttpGet("{staffId}")]
        public IActionResult GetStaffMember(int staffId)
        {
            try
            {
                if (!_staffCore.StaffMemberExists(staffId))
                {
                    return NotFound();
                }
                return Ok(_staffCore.GetStaffMember(staffId));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occured while retrieving a specific staff member");
                return StatusCode(500, errorMsg);
            }
        }

        [HttpPost("staff-editor", Name = "StaffEditor")]
        public IActionResult AddStaffMember([FromBody] AddStaffMemberDto sm)
        {
            StaffMemberDto staffMemberDto = Mapper.Map<StaffMemberDto>(sm);
            try
            {
                StaffMemberDto savedStaffMember = _staffCore.AddStaffMember(staffMemberDto);

                if (savedStaffMember == null)
                {
                    return BadRequest(ModelState);
                }
                return CreatedAtRoute("StaffEditor", new { staffId = savedStaffMember.Id }, savedStaffMember);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occured while trying to add a staff member");
                return StatusCode(500, errorMsg);
            }
        }

        [HttpPut("staff-editor/{staffId}")]
        public IActionResult UpdateStaffMemberName(int staffId,
            [FromBody] UpdateStaffMemberName sm)
        {
            try
            {
                bool updateSucceded = _staffCore.UpdateStaffMemberName(sm, staffId);

                if (!updateSucceded)
                {
                    return StatusCode(500, errorMsg);
                }
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occured while trying to perform a name change on the Staff Member with the id: {staffId}");
                ModelState.AddModelError("Update Name Failed: ", e.Message);
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost("status/inactive")]
        public IActionResult SetEmployeeStatusToInactive([FromBody] SetEmployeeStatusToInactiveDto sm)
        {
            try
            {
                if (!_staffCore.StaffMemberExists(sm.staffMemberId))
                {
                    ModelState.AddModelError("Not Found", "No staff member with the provided ID was found");
                    return BadRequest(ModelState);
                }

                _staffCore.SetInactiveStatus(sm.staffMemberId);

                return Ok();
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                ModelState.AddModelError("Setting inactive status", e.Message);
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost("main-job")]
        public IActionResult SetStaffMemberMainJob([FromBody] SetStaffMemberMainJobDto data)
        {
            try
            {
                if (!_staffCore.StaffMemberExists(data.StaffMemberId) && !_jobCore.JobExists(data.JobId))
                {
                    ModelState.AddModelError("Not Found", "One of the Ids provided was not found.");
                    return BadRequest(ModelState);
                }

                Job job = _jobCore.GetJob(data.JobId); 
                StaffMember staffMember = _staffCore.SetMainJob(data.StaffMemberId, job);
                return Ok();
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                ModelState.AddModelError("Error while setting main job", e.Message);
                return StatusCode(500, ModelState);
            }
        }


        [HttpDelete("staff-editor/{staffId}")]
        public IActionResult DeleteStaffMember(int staffId)
        {
            try
            {
                bool deleteSuccess = _staffCore.DeleteStaffMember(staffId);
               
                if (!deleteSuccess)
                {
                    return StatusCode(500, errorMsg);
                }
                return NoContent();
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"An error occured while trying to delete a staff member with the id: {staffId}");
                return StatusCode(500, errorMsg);
            }
        }
        
        [HttpGet("jobs")]
        public IActionResult GetAllJobs()
        {
            try
            {
                List<JobDto> jobs = _jobCore.GetAllJobs();
                return Ok(jobs);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                ModelState.AddModelError("Error Getting Jobs", e.Message);
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost("staff-member-jobs")]
        public IActionResult GetStaffMemberJobs([FromBody] GetStaffMemberJobsDto data)
        {
            try
            {
                if (!_staffCore.StaffMemberExists(data.StaffMemberId))
                {
                    return BadRequest("No staff member with the provided ID was found");
                }
                List<JobDto> jobs = _jobCore.GetStaffMemberJobs(data.StaffMemberId);
                return Ok(jobs);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                ModelState.AddModelError("Error Retreiving Approved Jobs", e.Message);
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost("add-approved-job")]
        public IActionResult AddApprovedJobToStaffMember([FromBody] EditStaffMemberApprovedRolesDto data)
        {
            try
            {
                if (!_staffCore.StaffMemberExists(data.StaffMemberId))
                {
                    return BadRequest("No staff member with the provided ID was found");
                }
                _jobCore.AddApprovedJobsToStaffMember(_staffCore.GetStaffMemberEntity(data.StaffMemberId), data.JobIds);
                return Ok();
            }
            catch (Exception e)
            {
                if (e.InnerException is InvalidOperationException)
                {
                    ModelState.AddModelError("Invalid Approved Job Operation", e.Message);
                    return BadRequest(ModelState);
                }

                if (e.InnerException is KeyNotFoundException)
                {
                    ModelState.AddModelError("ID Not Found", e.Message);
                    return BadRequest(ModelState);
                }
                _logger.LogError(e.Message);
                ModelState.AddModelError("Error Adding Approved Jobs", e.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPost("remove-job-approval")]
        public IActionResult RemoveJobApproval([FromBody] EditStaffMemberApprovedRolesDto data)
        {
            try
            {
                if (!_staffCore.StaffMemberExists(data.StaffMemberId))
                {
                    return BadRequest("No staff member with the provided ID was found");
                }
                _jobCore.RemoveJobApproval(data.StaffMemberId, data.JobIds);
                return Ok();
            }
            catch (Exception e)
            {
                if (e.InnerException is InvalidOperationException)
                {
                    ModelState.AddModelError("Invalid Job Removal Operation", e.Message);
                    return BadRequest(ModelState);
                }

                if (e.InnerException is KeyNotFoundException)
                {
                    ModelState.AddModelError("ID Not Found", e.Message);
                    return BadRequest(ModelState);
                }
                _logger.LogError(e.Message);
                ModelState.AddModelError("Error Removing Job Approval", e.Message);
                return StatusCode(500, ModelState);
            }
        }
    }
}
