using System.Text.RegularExpressions;

namespace Validation
{
    public static class Validaton
    {
        #region Fields

        static Regex m_regEmail;

        #endregion

        #region Ctor

        static Validaton()
        {
            m_regEmail = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        }

        #endregion

        #region Methods

        public static bool ValidateEmail(string email)
        {
            return m_regEmail.Match(email).Success;
        }

        #endregion
    }
}