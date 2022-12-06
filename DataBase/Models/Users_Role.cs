using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class Users_Role
    {
        #region PrimaryKey

        public Guid Id { get; set; }

        #region NavigationsProp

        public Guid UserId { get; set; }

        public User User { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        #endregion

        #endregion

        public override string ToString()
        {
            return $"{User} with role {Role}.";
        }

        
    }
}
