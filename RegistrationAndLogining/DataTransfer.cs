using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationAndLogining
{
    public static class DataTransfer
    {
        #region Fields

        static Dictionary<string, SecureString> _data;

        #endregion

        public static event Action<SecureString> OnPassChange;

        static DataTransfer()
        {
             _data = new Dictionary<string, SecureString>();
        }

        #region Methods

        public static void RegisterPass(SecureString pass, string key)
        {
            _data.Add(key, pass);
        }

        public static void ChangePassword(string key)
        {
            SecureString temp = null;

            _data.TryGetValue(key, out temp);

            OnPassChange?.Invoke(temp); 
        }

        public static void UnRegisterPass(string key)
        {
            _data.Remove(key);
        }

        #endregion

        #region Delegates

        #endregion
    }
}
