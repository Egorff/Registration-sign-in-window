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

        [MaxLength(20, ErrorMessage = "max length = 20")]
        [MinLength(10, ErrorMessage = "min length = 10")]
        [Required(ErrorMessage = "Property password mustn`t be null.")]
        public string Password { get; set; }

        #endregion

        #region Ctor

        public User(Guid id, string login, string email, string password)
        {
            Id = id;   
            Login = login;
            NormalizedLogin = login.ToUpper();
            Email = email;
            NormalizedEmail = email.ToUpper();
            Password = password;
        }

        public User()
        {

        }

        #endregion

        public override string ToString()
        {
            return $"{NormalizedLogin}, {NormalizedEmail}.";
        }
    }
}
