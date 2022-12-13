using DataBase.Models;

namespace DatabaseControllerLib
{
    public enum DatabaseOperations
    {

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

        #endregion
    }
}