using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TECHTALKFORUM.Data;
using TECHTALKFORUM.Models;

namespace TECHTALKFORUM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}

