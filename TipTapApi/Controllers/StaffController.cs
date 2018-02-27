using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipTapApi.Entities;
using TipTapApi.Models;
using TipTapApi.Models.StaffMemberDtos;
using TipTapApi.Services;
using TipTapApi.Validators;

namespace TipTapApi.Controllers
{
    [Route("staff")]
    public class StaffController : Controller
    {
        private IStaffMemberRepository _staffMemberRepository;
        private ILogger<StaffController> _logger;
        StaffMemberValidator Validator = new StaffMemberValidator();
        private string errorMsg = "An error occured while processing your request";
        public StaffController(IStaffMemberRepository staffMemberRepository, ILogger<StaffController> logger)
        {
            _staffMemberRepository = staffMemberRepository;
            _logger = logger;
        }

        [HttpGet()]
        public IActionResult GetAllStaff()
        {
            try
            {
                var staffEntity = _staffMemberRepository.GetStaffMembers();

                if (staffEntity.Count() == 0)
                {
                    return NotFound();
                }

                var staff = Mapper.Map<IEnumerable<StaffMemberDto>>(staffEntity);
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
                if (!_staffMemberRepository.StaffMemberExists(staffId))
                {
                    return NotFound();
                }
                var staffMemberEntity = _staffMemberRepository.GetStaffMember(staffId);
                var staffMember = Mapper.Map<StaffMemberDto>(staffMemberEntity);
                return Ok(staffMember);
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
            ValidationResult results = Validator.Validate(sm);
            bool validationSucceded = results.IsValid;

            if (!validationSucceded)
            {
                ModelState.AddModelError("Provided Parameters", "Please provide a valid Staff Member");
                return BadRequest(ModelState);
            }

            try
            {
                //Strip away the properties that StaffMemberDto shouldn't have by mapping it to 
                //StaffMemberForCreation
                StaffMemberForCreation smForCreation = Mapper.Map<StaffMemberForCreation>(sm);
                var staffMemberEntity = Mapper.Map<StaffMember>(smForCreation);
                _staffMemberRepository.CreateStaffMember(staffMemberEntity);

                if (!_staffMemberRepository.Save())
                {
                    return StatusCode(500, "An error occured while handling your request.");
                }

                var staffMember = Mapper.Map<StaffMemberDto>(staffMemberEntity);
                return CreatedAtRoute("StaffEditor", new { staffId = staffMember.Id }, staffMember);
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
            ValidationResult results = Validator.Validate(sm);
            bool validationSucceded = results.IsValid;


            if (!_staffMemberRepository.StaffMemberExists(staffId))
            {
                return NotFound();
            }

            if (!validationSucceded)
            {
                ModelState.AddModelError("Provided Parameters", "Please provide a valid Staff Member");
                return BadRequest(ModelState);
            }
            try
            {
                //Strip incoming data properties from StaffMemberDto that shouldn't be there
                var smForUpdate = Mapper.Map<StaffMemberForUpdate>(sm);

                var staffToUpdate = _staffMemberRepository.GetStaffMember(staffId);
                Mapper.Map(smForUpdate, staffToUpdate);
                var updatedStaffMember = Mapper.Map<StaffMemberDto>(staffToUpdate);
                if (!_staffMemberRepository.Save())
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

        [HttpPatch("staff-editor/{staffId}")]
        public IActionResult PartiallyUpdateStaffMember(int staffId, 
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
                StaffMemberDto smForValidation = Mapper.Map<StaffMemberDto>(patchedStaffMember);
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

        [HttpDelete("staff-editor/{staffId}")]
        public IActionResult DeleteStaffMember(int staffId)
        {
            if (!_staffMemberRepository.StaffMemberExists(staffId))
            {
                return NotFound();
            }

            try
            {

                var staffMemberToDelete = _staffMemberRepository.GetStaffMember(staffId);
                _staffMemberRepository.DeleteStaffMember(staffMemberToDelete);

                if (!_staffMemberRepository.Save())
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
