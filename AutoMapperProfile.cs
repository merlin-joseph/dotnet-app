using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using learn.Dtos.Course;

namespace learn
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile(){
            CreateMap<Course, GetCourseDto>();
            CreateMap<AddCourseDto, Course>();
            CreateMap<UpdateCourseDto, Course>();

        }
        
    }
}