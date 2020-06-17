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
using System.Linq;

namespace afrotutor.webapi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ClassesController : ControllerBase
    {
        private IClassService _classService;
        private IUserClassService _userClassService;
        private IMapper _mapper;

        public ClassesController(
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
                foreach (var item in classesDto)
                {
                    item.IsOwner = true;
                }

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
                var @class = _classService.GetById(id);
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

        [HttpGet("SearchClassesNU/{name}/{userId}")]
        public IActionResult SearchClasses(string name, int userId)
        {
            try
            {
                Log.Information($"Attempting to get class with name: {name}");
                var classes = _classService.SearchClasses(name);
                List<ClassDto> classesDto = _mapper.Map<List<ClassDto>>(classes);

                var myClasses = _userClassService.GetUserClasses(userId);
                
                foreach (var item in myClasses)
                {
                    var @class = classesDto.FirstOrDefault(c => c.Id == item.Id);
                    if(@class != null)
                        @class.Subscribed = true;
                        @class.UserClassId = item.Id;
                }

                foreach (var item in classesDto)
                {
                    if(item.UserId == userId)
                    item.IsOwner = true;
                }
                
                
                return Ok(classesDto);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                Log.Error($"Failed to get Class with name: {name}");
                Log.Error(ex.StackTrace.ToString());
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("SearchClassesNLU/{name}/{location}/{userId}")]
        public IActionResult SearchClasses(string name, string location, int userId)
        {
            try
            {
                Log.Information($"Attempting to get class with name: {name} and location: {location}");
                var classes = _classService.SearchClasses(name, location);
                List<ClassDto> classesDto = _mapper.Map<List<ClassDto>>(classes);

                var myClasses = _userClassService.GetUserClasses(userId);
                
                foreach (var item in myClasses)
                {
                    var @class = classesDto.FirstOrDefault(c => c.Id == item.Id);
                    if(@class != null)
                        @class.Subscribed = true;
                        @class.UserClassId = item.Id;
                }

                foreach (var item in classesDto)
                {
                    if(item.UserId == userId)
                    item.IsOwner = true;
                }
                
                return Ok(classesDto);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                Log.Error($"Failed to get Class with name: {name}");
                Log.Error(ex.StackTrace.ToString());
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("SearchClassesNSU/{name}/{startDate}/{userId}")]
        public IActionResult SearchClasses(string name, DateTime startDate, int userId)
        {
            try
            {
                Log.Information($"Attempting to get class with name: {name}");
                var classes = _classService.SearchClasses(name, startDate);
                List<ClassDto> classesDto = _mapper.Map<List<ClassDto>>(classes);

                var myClasses = _userClassService.GetUserClasses(userId);
                
                foreach (var item in myClasses)
                {
                    var @class = classesDto.FirstOrDefault(c => c.Id == item.Id);
                    if(@class != null)
                        @class.Subscribed = true;
                        @class.UserClassId = item.Id;
                }
                
                foreach (var item in classesDto)
                {
                    if(item.UserId == userId)
                    item.IsOwner = true;
                }
                
                return Ok(classesDto);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                Log.Error($"Failed to get Class with name: {name}");
                Log.Error(ex.StackTrace.ToString());
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("SearchClassesNSEU/{name}/{startDate}/{endDate}/{userId}")]
        public IActionResult SearchClasses(string name, DateTime startDate, DateTime endDate, int userId)
        {
            try
            {
                Log.Information($"Attempting to get class with name: {name}");
                var classes = _classService.SearchClasses(name, startDate, endDate);
                List<ClassDto> classesDto = _mapper.Map<List<ClassDto>>(classes);

                var myClasses = _userClassService.GetUserClasses(userId);
                
                foreach (var item in myClasses)
                {
                    var @class = classesDto.FirstOrDefault(c => c.Id == item.Id);
                    if(@class != null)
                        @class.Subscribed = true;
                        @class.UserClassId = item.Id;
                }
                
                foreach (var item in classesDto)
                {
                    if(item.UserId == userId)
                    item.IsOwner = true;
                }
                
                return Ok(classesDto);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                Log.Error($"Failed to get Class with name: {name}");
                Log.Error(ex.StackTrace.ToString());
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("SearchClassesNLSEU/{name}/{location}/{startDate}/{endDate}/{userId}")]
        public IActionResult SearchClasses(string name, string location, DateTime startDate, DateTime endDate, int userId)
        {
            try
            {
                Log.Information($"Attempting to get class with name: {name}");
                var classes = _classService.SearchClasses(name, location, startDate, endDate);
                List<ClassDto> classesDto = _mapper.Map<List<ClassDto>>(classes);

                var myClasses = _userClassService.GetUserClasses(userId);
                
                foreach (var item in myClasses)
                {
                    var @class = classesDto.FirstOrDefault(c => c.Id == item.Id);
                    if(@class != null)
                        @class.Subscribed = true;
                        @class.UserClassId = item.Id;
                }
                
                foreach (var item in classesDto)
                {
                    if(item.UserId == userId)
                    item.IsOwner = true;
                }
                
                return Ok(classesDto);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                Log.Error($"Failed to get Class with name: {name}");
                Log.Error(ex.StackTrace.ToString());
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody]ClassDto classDto)
        {
            // map dto to entity
            var @class  = _mapper.Map<Class>(classDto);

            try
            {
                // save
                Log.Information($"Attempting to save class: {@class.Subject} from user: {@class.UserId}");
                var newClass = _classService.Create(@class);
                var newclassDto = _mapper.Map<ClassDto>(newClass);
                newclassDto.IsOwner = true;

                return Ok(newclassDto);

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
        public IActionResult Update(int id, [FromBody]ClassDto classDto)
        {
            // map dto to entity and set id
            var @class = _mapper.Map<Class>(classDto);
            @class.Id = id;

            try 
            {
                // save 
                Log.Information($"Attempting to save class: {@class.Subject} for user: {@class.UserId}");
                var updatedClass = _classService.Update(@class);
                Log.Information("Update Successful");
                var updatedClassDto = _mapper.Map<ClassDto>(updatedClass);
                updatedClassDto.IsOwner = true;
                return Ok(updatedClassDto);
            } 
            catch(AppException ex)
            {
                // return error message if there was an exception
                Log.Error($"Failed to update Class with id: {@class.Id}");
                Log.Error(ex.StackTrace.ToString());
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("cancel/{id}")]
        public IActionResult Cancel(int id)
        {
            try
            {
                Log.Information($"Attempting to Cancel class: {id}");
                var @class = _classService.Cancel(id);
                Log.Information($"Successfully Cancelled class {id}");
                var classDto = _mapper.Map<ClassDto>(@class);
                classDto.IsOwner = true;
                return Ok(classDto);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("activate/{id}")]
        public IActionResult Activate(int id)
        {
            try
            {
                Log.Information($"Attempting to Activate class: {id}");
                var @class = _classService.Activate(id);
                Log.Information($"Successfully Activated class {id}");
                var classDto = _mapper.Map<ClassDto>(@class);
                classDto.IsOwner = true;
                return Ok(classDto);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Log.Information($"Attempting to delete class: {id}");
                _classService.Delete(id);
                Log.Information($"Successfully Deleted class {id}");
                return Ok(id);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}