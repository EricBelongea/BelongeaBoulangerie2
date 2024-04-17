using BelongeaBoulangerie.DataContext.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelongeaBoulangerie.DataContext.Utils
{
    public class UserService
    {
        private readonly BoulangerieContext _context;
        public UserService(BoulangerieContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserService(User user)
        {
            if (!await IsUserNameUnique(user.UserName))
            {
                throw new InvalidOperationException("Username must be unique");
            }

            var newUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }

        private async Task<bool> IsUserNameUnique(string username)
        {
            var taken = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            return taken == null;
        }

        public async Task<User> GetUserByUserName(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
            {
                throw new Exception("Username not found");
            }
            return user;
        }

        //public async Task<User> UpdateUser(User user)
        //{
        //    var existingUser = await _context.Users.FindAsync(user.UserId);
        //    if (existingUser == null)
        //    {
        //        throw new DirectoryNotFoundException("User Not Found");
        //    }

        //    existingUser.FirstName = user.FirstName;
        //    existingUser.LastName = user.LastName;
        //    existingUser.Email = user.Email;
        //    existingUser.UserName = user.UserName;

        //    return existingUser;
        //}
    }
}
