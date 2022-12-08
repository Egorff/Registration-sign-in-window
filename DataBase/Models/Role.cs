using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class Role
    {
        #region Prop

        [Key]
        [Required(ErrorMessage = "Property id mustn't be null.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Property role name mustn't be null.")]
        public string RoleName { get; set; }

        public List<Users_Role> User_Role { get; set; }

        #endregion



        #region Ctor

        public Role(string roleName)
        {
            RoleName = roleName;
        }

        #endregion

        public override string ToString()
        {
            return $"{RoleName}";
        }
    }
}
