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
        public static bool CheckCode(int[] code, string codee)
        {
            if (string.IsNullOrEmpty(codee))
            {
                return false;
            }

            if (code.Length != codee.Length)
            {
                return false;
            }

            for (int i = 0; i < code.Length; i++)
            {
                if ((double)code[i] != Char.GetNumericValue(codee[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ValidateCode(string code, out string error)
        {
            error = string.Empty;

            if (string.IsNullOrEmpty(code))
            {
                error = "Field is mustn`t be null";

                return false;
            }

            int length = code.Length;

            for (int i = 0; i < length; i++)
            {
                if (!char.IsNumber(code[i]))
                {
                    error = $"Wrong symbol at the {i+1} position.";

                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}