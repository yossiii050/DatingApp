using API.Data;
using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UserController(IUserRepository userRepository,IMapper mapper) : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await userRepository.GetUsersAsync();
            var userToReturn = mapper.Map<IEnumerable<MemberDto>>(users);
            return Ok(userToReturn);
        }

        
        [HttpGet("{username}")] // /api/users/3
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var user = await userRepository.GetUserByUsernamesync(username);
            if(user==null)
                return NotFound();

            return mapper.Map<MemberDto>(user);
        }
    }
}
