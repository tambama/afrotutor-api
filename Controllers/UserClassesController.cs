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
    public class UserClassesController : ControllerBase
    {
        private IClassService _classService;
        private IUserClassService _userClassService;
        private IMapper _mapper;

        public UserClassesController(
            IClassService classService,
            IUserClassService userClassService,
            IMapper mapper)
        {
            _classService = classService;
            _userClassService = userClassService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            Log.Information("Attempting to get all Classes");
            var classes = _classService.GetAll();
            var classesDto = _mapper.Map<IList<ClassDto>>(classes);
            return Ok(classesDto);
        }

        [HttpGet("GetUserClasses/{userId}")]
        public IActionResult GetUserClasses(int userId)
        {
            try
            {
                Log.Information($"Attempting to get Class for user: {userId}");
                var classes = _classService.GetTutorClasses(userId);
                List<ClassDto> classesDto = _mapper.Map<List<ClassDto>>(classes);

                // get user classes
                var userClasses = _userClassService.GetUserClasses(userId);
                List<ClassDto> userClassesDto = _mapper.Map<List<ClassDto>>(userClasses);
                classesDto.AddRange(userClassesDto);

                return Ok(classesDto);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                Log.Error($"Failed to get Classes for user: {userId}");
                Log.Error(ex.StackTrace.ToString());
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                Log.Information($"Attempting to get class with id: {id}");
                var @class = _userClassService.GetById(id);
                ClassDto classDto = _mapper.Map<ClassDto>(@class);
                if (@class != null)
                    return Ok(@class);
                
                Log.Error($"Class: {id} not found");
                return NotFound();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                Log.Error($"Failed to get Class with id: {id}");
                Log.Error(ex.StackTrace.ToString());
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpPost("create")]
        public IActionResult Create([FromBody]UserClassDto classDto)
        {
            // map dto to entity
            var @class  = _mapper.Map<UserClass>(classDto);

            try
            {
                // save
                Log.Information($"Attempting to save class");
                var newClass = _userClassService.Create(@class);
                return Ok(newClass);

            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                Log.Error($"Failed to save Class");
                Log.Error(ex.StackTrace.ToString());
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserClassDto classDto)
        {
            // map dto to entity and set id
            var @class = _mapper.Map<UserClass>(classDto);
            @class.Id = id;

            try 
            {
                // save 
                Log.Information($"Attempting to save class");
                var userClassDto = _mapper.Map<UserClassDto>(_userClassService.Update(@class));
                Log.Information("Update Successful");
                return Ok(userClassDto);
            } 
            catch(AppException ex)
            {
                // return error message if there was an exception
                Log.Error($"Failed to update Class with id: {@class.Id}");
                Log.Error(ex.StackTrace.ToString());
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Log.Information($"Attempting to delete class: {id}");
                _userClassService.Delete(id);
                Log.Information($"Successfully Deleted class {id}");
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}