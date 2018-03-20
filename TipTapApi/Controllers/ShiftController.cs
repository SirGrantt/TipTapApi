using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Common.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Common.DTOs;
using Persistence.Repositories;
using Common.DTOs.ShiftDtos;
using Domain.ShiftManager;
using AutoMapper;

namespace TipTapApi.Controllers
{
    [Route("shift")]
    public class ShiftController : Controller
    {
        IShiftRepository _repo;
        private ILogger<ShiftController> _logger;
        private ShiftCore _shiftCore;

        public ShiftController(IShiftRepository repo, ILogger<ShiftController> logger)
        {
            _logger = logger;
            _repo = repo;
            _shiftCore = new ShiftCore(_repo);
        }

        [HttpPost()]
        public IActionResult GetShift([FromBody] ShiftCreationDataDto data)
        {
            //When a client makes a GetShift request they do not care if one for that day's current shift has been made before,
            //they only want the shift related to the date and lunch or dinner specification they have picked. So if the
            //date they are viewing has been made before then return that to them, otherwise create one for that date and
            //return that.
            try
            {
                DateTime convertedDataForQuery = Convert.ToDateTime(data.ShiftDate);

                if (data.LunchOrDinner.ToLower().Trim() != "lunch" && data.LunchOrDinner.ToLower().Trim() != "dinner")
                {
                    return BadRequest("The provided lunch or dinner specification must either be 'lunch' or 'dinner'.");
                }

                Shift shift = _shiftCore.GetShift(convertedDataForQuery, data.LunchOrDinner);

                if (shift == null)
                {
                    shift = _shiftCore.CreateShift(convertedDataForQuery, data.LunchOrDinner);
                }

                ShiftDto shiftDto = Mapper.Map<ShiftDto>(shift);

                return Ok(shiftDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occured when trying to retrieve a shift.");
                return StatusCode(500, "An error occured while processing your request.");
            }
        }

        
        [HttpPost("pay-period")]
        public IActionResult GetShiftsFromRange([FromBody] GetShiftsBetweenSelectedDates dates)
        {
            DateTime start;
            DateTime end;
            try
            {
                start = Convert.ToDateTime(dates.StartDate);
                end = Convert.ToDateTime(dates.EndDate);
            }
            catch
            {
                return BadRequest("One or both of the provided values for start and end date were not valid.");
            }

            try
            { 
                IEnumerable<ShiftDto> shifts = Mapper.Map<IEnumerable<ShiftDto>>(_shiftCore.GetShiftsFromRange(start, end));

                List<ShiftDto> shiftsToReturn = shifts.ToList();
                return Ok(shiftsToReturn);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting the shifts from a specified range in the GetShiftsFromRange method of the ShiftsController");
                return StatusCode(500, "Something went wrong while trying to retrieve shifts from the specified range");
            }
        }
        

    }
}