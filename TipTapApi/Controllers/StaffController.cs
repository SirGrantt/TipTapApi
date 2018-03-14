using Application;
using Application.DTOs.StaffMemberDtos;
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
using TipTapApi.Services;

namespace TipTapApi.Controllers
{
    [Route("staff")]
    public class StaffController : Controller
    {
        IStaffMemberRepository _repo;
        private ILogger<StaffController> _logger;
        private StaffMembersCore _staffCore;
        private string errorMsg = "An error occured while processing your request";
        public StaffController(IStaffMemberRepository repo, ILogger<StaffController> logger)
        {
            _repo = repo;
            _logger = logger;
            _staffCore = new StaffMembersCore(_repo);
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
            catch(Exception e)
            {
                _logger.LogError(e, "An error occured while retrieving a specific staff member");
                return StatusCode(500, errorMsg);
            }
        }
        
        [HttpPost("staff-editor", Name = "StaffEditor")]
        public IActionResult AddStaffMember([FromBody] StaffMemberDto sm)
        {
            try
            {
                StaffMemberDto savedStaffMember = _staffCore.AddStaffMember(sm);

                if(savedStaffMember == null)
                {
                    return BadRequest(ModelState);
                }
                return CreatedAtRoute("StaffEditor", new { staffId = savedStaffMember.Id }, savedStaffMember);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "An error occured while trying to add a staff member");
                return StatusCode(500, errorMsg);
            }
        }
        
        [HttpPut("staff-editor/{staffId}")]
        public IActionResult UpdateStaffMember(int staffId, 
            [FromBody] StaffMemberDto sm)
        {
            try
            {
                bool updateSucceded = _staffCore.UpdateStaffMember(sm, staffId);

                if (!updateSucceded)
                {
                    return StatusCode(500, errorMsg);
                }
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occured while trying to perform a PUT update on a staff member with the id: {staffId}");
                return StatusCode(500, errorMsg);
            }
        }
        /*
        [HttpPatch("staff-editor/{staffId}")]
        public IActionResult ChangeStaffMemberNameCommand(int staffId, 
            [FromBody] JsonPatchDocument<StaffMemberForUpdate> s)
        {
            if (s == null)
            {
                return BadRequest();
            }

            if (!_staffMemberRepository.StaffMemberExists(staffId))
            {
                return NotFound();
            }

            try
            {
                var staffMemberEntity = _staffMemberRepository.GetStaffMember(staffId);
                var staffMemberToPatch = Mapper.Map<StaffMemberForUpdate>(staffMemberEntity);
                s.ApplyTo(staffMemberToPatch, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //Validate that the items being patched are given valid parameters
                StaffMemberForUpdate patchedStaffMember = staffMemberToPatch;
                StaffMemberDtoOLD smForValidation = Mapper.Map<StaffMemberDtoOLD>(patchedStaffMember);
                ValidationResult result = Validator.Validate(smForValidation);
                bool resultIsValid = result.IsValid;
                if (!resultIsValid)
                {
                    ModelState.AddModelError("Provided Parameters", "Please provide valid Staff Member properites.");
                    return BadRequest(ModelState);
                }

                Mapper.Map(staffMemberToPatch, staffMemberEntity);

                if (!_staffMemberRepository.Save())
                {
                    return StatusCode(500, "An error occured while processing your request.");
                }

                return NoContent();
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"An error occured while trying to perform a PATCH update on a staff member with the id: {staffId}");
                return StatusCode(500, errorMsg);
            }
        }
        */
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
    }
}
