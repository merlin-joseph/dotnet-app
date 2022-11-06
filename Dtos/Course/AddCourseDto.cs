using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learn.Dtos.Course
{
    public class AddCourseDto
    {
        public string Name {get; set;} = "Intro";

        public int Rate {get; set;} = 1000;

        public string Duration {get; set;} = "1 hour";
 
        public  courseType type {get; set;} = courseType.Kg;
         public string Url {get; set;} = string.Empty;
        public string Description {get;set;} = string.Empty;

        public String Genre {get;set;} = string.Empty;

    }
}