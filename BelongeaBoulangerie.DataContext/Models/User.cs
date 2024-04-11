using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelongeaBoulangerie.DataContext.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [Required, Column("EmailAddress"), MaxLength(50)] // Will rename column in db, but not here in the model. 
        public string Email { get; set; }
        public string FullName => FirstName + " " + LastName;

        [Required, MaxLength(50)]
        //public string UserName { get; set; }
        public string UserName // This is a backing field. 
        {
            get
            {
                return _validUsername;
            }
        }
        private string _validUsername;

        public void SetUsername(string username)
        {
            string specialCharacters = ".*(?=.*[@#$%^&+=(){}<>!~_*?]).*$";

            bool usernameContainsSpecialCharacters = username.Any(c => specialCharacters.Contains(c));
            if (usernameContainsSpecialCharacters)
            {
                throw new ArgumentException("Special characters are not allowed in username", username);
            }

            _validUsername = username;
        }
    }
}
