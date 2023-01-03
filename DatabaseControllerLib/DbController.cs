using DataBase.Models;
using DataBase.Utilities;
using SecureStringExtentionsLib;
using SHA512;
using System.Security;
using System.Text;

namespace DatabaseControllerLib
{
    public enum DatabaseOperations
    {
        Register = 0, Login
    }

    public class DbController : ControllerBaseLib.ControllerBase<DatabaseOperations>
    {
        #region Fields

        UsersDb usersDb;

        #endregion

        #region Properties



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

        #endregion
    }
}