using API.Data;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")] ///api.users
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users=await _context.Users.ToListAsync();
            
            return users;
        }

        [HttpGet("{id:int}")] // /api/users/3
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var user=await _context.Users.FindAsync(id);
            if(user==null)
                return NotFound();

            return user;
        }
    }
}
