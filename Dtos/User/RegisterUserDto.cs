using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learn.Dtos.User
{
    public class RegisterUserDto
    {
        public string UserName{get;set;}= string.Empty;
        public string Password{get;set;}= string.Empty;
    }
}