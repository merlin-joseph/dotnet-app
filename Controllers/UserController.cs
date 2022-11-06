using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using learn.Data;
using learn.Dtos.User;

namespace learn.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class UserController: ControllerBase
    {
        private readonly IAuthRepository _auth;
        public UserController(IAuthRepository auth){
            _auth = auth;
        }

    [HttpPost("register")]
    public async Task<ActionResult<ServiceResponse<int>>> Register(RegisterUserDto request){

        var response = await _auth.Register(
            new User{UserName = request.UserName}, request.Password
        );
        if(!response.Success){
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto request){

        var response = await _auth.Login(
            request.UserName, request.Password
        );
        if(!response.Success){
            return BadRequest(response);
        }
        return Ok(response);
    }

    }
}