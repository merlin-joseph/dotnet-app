using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using learn.Services.CourseService;
using learn.Dtos.Course;

namespace learn.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    
    public class CourseController : ControllerBase
    {
       
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService){
            _courseService = courseService;
        }

        [HttpGet]

        public async Task<ActionResult<ServiceResponse<List<GetCourseDto>>>> Get([FromQuery] QueryParams queryParams){
            return Ok( await _courseService.GetCourses(queryParams));
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<ServiceResponse<GetCourseDto>>> GetById(int id) {
            return Ok(await _courseService.GetCourseById(id));
        }

        [HttpPost]

        public async Task<ActionResult<ServiceResponse<List<GetCourseDto>>>> CreateCourse(AddCourseDto newCourse){
            return Ok(await _courseService.CreateCourse(newCourse)) ;
        }
        
        [HttpPut]

        public async Task<ActionResult<ServiceResponse<GetCourseDto>>> UpdateCourse(UpdateCourseDto updatedCourse){
           var response = await _courseService.UpdateCourse(updatedCourse);
           if(response.Data == null){
            return NotFound(response);
           }
            return Ok(response);
        }

        [HttpDelete("{id}")]
         public async Task<ActionResult<ServiceResponse<GetCourseDto>>> DeleteCourse(int id){
           var response = await _courseService.DeleteCourse(id);
           if(response.Data == null){
            return NotFound(response);
           }
            return Ok(response);
        }
    }
}