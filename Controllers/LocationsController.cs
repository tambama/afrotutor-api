using System;
using System.Collections.Generic;
using afrotutor.webapi.Dtos;
using afrotutor.webapi.Entities;
using afrotutor.webapi.Helpers;
using afrotutor.webapi.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace afrotutor.webapi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LocationsController : ControllerBase
    {
        private ILocationService _locationService;
        private IMapper _mapper;

        public LocationsController(
            ILocationService locationService,
            IMapper mapper)
        {
            _locationService = locationService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            Log.Information("Attempting to get all Locations");
            var locations = _locationService.GetAll();
            var locationsDto = _mapper.Map<IList<LocationDto>>(locations);
            return Ok(locationsDto);
        }

        [HttpGet("GetUserLocations/{userId}")]
        public IActionResult GetUserLocations(int userId)
        {
            try
            {
                Log.Information($"Attempting to get Location for user: {userId}");
                var locations = _locationService.GetUserLocations(userId);
                var locationsDto = _mapper.Map<IList<LocationDto>>(locations);
                return Ok(locationsDto);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                Log.Error($"Failed to get Locations for user: {userId}");
                Log.Error(ex.StackTrace.ToString());
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                Log.Information($"Attempting to get location with id: {id}");
                var location = _locationService.GetById(id);
                if (location != null)
                    return Ok(location);
                
                Log.Error($"Location: {id} not found");
                return NotFound();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                Log.Error($"Failed to get Location with id: {id}");
                Log.Error(ex.StackTrace.ToString());
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody]LocationDto locationDto)
        {
            // map dto to entity
            var location  = _mapper.Map<Location>(locationDto);

            try
            {
                // save
                Log.Information($"Attempting to save location: {location.Name} from user: {location.UserId}");
                _locationService.Create(location);
                return Ok(location);

            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                Log.Error($"Failed to save Location");
                Log.Error(ex.StackTrace.ToString());
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]LocationDto locationDto)
        {
            // map dto to entity and set id
            var location = _mapper.Map<Location>(locationDto);
            location.Id = id;

            try 
            {
                // save 
                Log.Information($"Attempting to save location: {location.Name} for user: {location.UserId}");
                _locationService.Update(location);
                Log.Information("Update Successful");
                return Ok();
            } 
            catch(AppException ex)
            {
                // return error message if there was an exception
                Log.Error($"Failed to update Location with id: {location.Id}");
                Log.Error(ex.StackTrace.ToString());
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Log.Information($"Attempting to delete location: {id}");
            _locationService.Delete(id);
            Log.Information($"Successfully Deleted location {id}");
            return Ok();
        }
    }
}