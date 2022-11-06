using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learn.Models
{
    public class User
    {
        public int Id {get; set;}
        public string UserName{get;set;}= string.Empty;
        public byte[] passwordHash {get;set;} 
        public byte[] passwordSalt{get;set;}
        public List<Course>? Courses{get;set;}
        
    }
}