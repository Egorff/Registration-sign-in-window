using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class User
    {
        #region Prop

        [Required(ErrorMessage = "Property id mustn`t be null.")]
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Property login mustn`t be null.")]
        public string Login { get; set; }

        public string NormalizedLogin { get; set; }

        [Required(ErrorMessage = "Property e-mail mustn`t be null.")]
        public string Email { get; set; }

        public string NormalizedEmail { get; set; }

        [Required(ErrorMessage = "Property password mustn`t be null.")]
        public string Password { get; set; }

        public List<Users_Role> User_Role { get; set; }

        public Salt Salt { get; set; } 

        #endregion



        #region Ctor

        public User(Guid id, string login, string email, string password, Salt salt)
        {
            Id = id;   
            Login = login;
            NormalizedLogin = login.ToUpper();
            Email = email;
            NormalizedEmail = email.ToUpper();
            Password = password;
            Salt = salt;
        }

        public User()
        {

        }

        #endregion

        public override string ToString()
        {
            return $"{User_Role[0]}, {Email}, {Login}.";
        }
    }
}
