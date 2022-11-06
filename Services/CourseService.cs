using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learn.Dtos.Course;
using AutoMapper;
using learn.Data;
using Microsoft.EntityFrameworkCore;

namespace learn.Services.CourseService
{

    public class CourseService : ICourseService
    {
         

        private readonly IMapper _mapper;
         private readonly DataContext _context;

        public CourseService(IMapper mapper,DataContext context ){
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<List<GetCourseDto>>> CreateCourse(AddCourseDto newCourse)
        {
            var serviceResponse = new ServiceResponse<List<GetCourseDto>>();
            Course course = _mapper.Map<Course>(newCourse);
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Courses.Select(c => _mapper.Map<GetCourseDto>(c)).ToListAsync();
            return serviceResponse;     
        }

        public async Task<ServiceResponse<GetCourseDto>> GetCourseById(int id)
        {
           
            var serviceResponse = new ServiceResponse<GetCourseDto>();
            var course =  await _context.Courses.FirstOrDefaultAsync(c => c.Id == id) ;
            serviceResponse.Data = _mapper.Map<GetCourseDto>(course);
            return serviceResponse;
      }

        public async Task<ServiceResponse<List<GetCourseDto>>> GetCourses(QueryParams queryParams)
        {
           
            var serviceResponse = new ServiceResponse<List<GetCourseDto>>();
            var dbCourses = await _context.Courses.ToListAsync();

            if(queryParams != null && queryParams.genre != string.Empty ){
                 dbCourses = dbCourses.Where(c => c.Genre.ToLower() == queryParams.genre.ToLower()).ToList();
            }
            serviceResponse.Data = dbCourses.Select(c => _mapper.Map<GetCourseDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCourseDto>> UpdateCourse(UpdateCourseDto updatedCourse){
            var serviceResponse = new ServiceResponse<GetCourseDto>();
            try{
            var course =  await _context.Courses.FirstOrDefaultAsync(c => c.Id == updatedCourse.Id) ;
            course.Name = updatedCourse.Name;
            course.Rate = updatedCourse.Rate;
            course.Duration = updatedCourse.Duration;
            course.type = updatedCourse.type;
            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<GetCourseDto>(course);
            } catch(Exception ex){
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;


        }

         public async Task<ServiceResponse<List<GetCourseDto>>> DeleteCourse(int id)
        {
           
            var serviceResponse = new ServiceResponse<List<GetCourseDto>>();
            try {
            var course =   await _context.Courses.FirstAsync(c => c.Id == id) ;
             _context.Courses.Remove(course);
             await _context.SaveChangesAsync();
            serviceResponse.Data = _context.Courses.Select(c => _mapper.Map<GetCourseDto>(c)).ToList();
            }catch(Exception ex){
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
      }

    }
}