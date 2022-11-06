using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learn.Dtos.Course;

namespace learn.Services.CourseService
{
    public interface ICourseService
    {
        Task<ServiceResponse<List<GetCourseDto>>> GetCourses(QueryParams parameters);

        Task<ServiceResponse<GetCourseDto>> GetCourseById(int id);

        Task<ServiceResponse<List<GetCourseDto>>> CreateCourse(AddCourseDto newCourse);

        Task<ServiceResponse<GetCourseDto>> UpdateCourse(UpdateCourseDto newCourse);

        Task<ServiceResponse<List<GetCourseDto>>> DeleteCourse(int id);


    }
}