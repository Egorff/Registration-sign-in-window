using ControllerBaseLib.Enums;
using DataBase.Models;
using DataBase.Utilities;
using Microsoft.EntityFrameworkCore;
using SecureStringExtentionsLib;
using SHA512;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace DatabaseControllerLib
{
    public enum DatabaseOperations
    {
        Register = 0, Login, ChangePassword
    }

    public class DbController : ControllerBaseLib.ControllerBase<DatabaseOperations>
    {
        #region Fields

        UsersDb usersDb;

        #endregion

        #region Ctor

        public DbController()
        {
            usersDb = new UsersDb();
        }

        #endregion

        #region Methods

        public bool IsLoginExists(string login)
        {
            var e = from p in usersDb.User where p.NormalizedLogin.Equals(login.ToUpper()) select p;

            return e.Count() > 0;
        }

        public bool IsEmailExists(string email)
        {
            var e = from p in usersDb.User where p.NormalizedEmail.Equals(email.ToUpper()) select p;

            return e.Count() > 0;
        }

        public void RegisterUser(string login, string email, SecureString password)
        {
            ExecuteFunc(DatabaseOperations.Register, () =>
            {
                Salt salt = new Salt(Guid.NewGuid(), SaltGen.GenerateSalt(password.Length));

                var pass = password.GetBytesAccordingToEncoding(new ASCIIEncoding());

                var saltedPass = HASH.HashPass(pass, salt.ToBytes(new ASCIIEncoding()), new ASCIIEncoding());

                User user = new User(Guid.NewGuid(), login, email, saltedPass, salt);

                Users_Role UR = new Users_Role(Guid.NewGuid(), user.Id, 1);

                usersDb.Salt.Add(salt);

                usersDb.User.Add(user);

                usersDb.Users_Role.Add(UR);

                return usersDb.SaveChanges();
            });
        }

        public void LoginUser(string login, SecureString password)
        {
            ExecuteFunc(DatabaseOperations.Login, () =>
            {
                var u = (from p in usersDb.User where p.NormalizedLogin.Equals(login.ToUpper())
                         select p).Include(y => y.Salt).Include(t => t.User_Role).First();

                var role = (from p in usersDb.Role where p.Id == u.User_Role[0].RoleId select p).First();

                u.User_Role[0].Role = role;

                //var sUi = (from p in usersDb.Salt where p.UserId.Equals(u.Id) select p).First();

                //var RoleId = (from p in usersDb.Users_Role where p.UserId.Equals(u.Id) select p.RoleId).First();

                //var role = (from p in usersDb.Role where p.Id == RoleId select p.RoleName).First();

                var pass = password.GetBytesAccordingToEncoding(new ASCIIEncoding());

                var HashPass = HASH.HashPass(pass, u.Salt.ToBytes(new ASCIIEncoding()), new ASCIIEncoding());

                if (u.Password.Equals(HashPass))
                {
                    return u;
                }

                return null;
            });
        }

        //public void ChangePassword(SecureString password, string email)
        //{
        //    ExecuteFunc(DatabaseOperations.ChangePassword, () =>
        //    {
        //        var u = (from p in usersDb.User where p.NormalizedEmail.Equals(email.ToUpper()) select p).Include(y => y.Password).First();

        //        return null;
        //    });

        //}

        #endregion
    }
}