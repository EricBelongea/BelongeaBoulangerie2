using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BelongeaBoulangerie.DataContext.Models;
using BelongeaBoulangerie.DataContext.Utils;

namespace BelongeaBoulangerie2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BoulangerieContext _context;
        private readonly UserService _userService;

        public UsersController(BoulangerieContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id); // this shouldn't work since the user has a UserId not id. 

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/Users/Username
        [HttpGet("username/{userName}")]
        public async Task<ActionResult<User>> GetUserName(string userName)
        {
            try
            {
                var user = await _userService.GetUserByUserName(userName);
                return user;
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser(User user)
        {
            try
            {
                var existingUser = await _context.Users.FindAsync(user.UserId);

                if (existingUser == null)
                {
                    return NotFound("User not found");
                }

                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.UserName = user.UserName;

                _context.Users.Update(existingUser);
                _context.SaveChanges();
                return Content("User Updated");
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            catch (DbUpdateException)
            {
                return BadRequest("Username or Email already in use.");
            }
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                var newUser = await _userService.CreateUserService(user);
                return CreatedAtAction("GetUser", new { id = user.UserId }, user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUser(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
