using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBaseLib.VM;

namespace RegistrationAndLogining.ViewModels.Pages
{
    public class RegistrationPageViewModel : ViewModelBase
    {
        #region Fields

        string m_login;



        #endregion

        #region Prop

        public string Login 
        { 
            get => m_login; 

            set => Set(ref m_login, value); 
        
        }

        #endregion

        #region Ctor

        public RegistrationPageViewModel()
        {
            
        }

        #endregion

        #region IDataErrorInfo

        public override string this[string columnName]
        {
            get 
            { 
                string error = string.Empty;

                return error;
            }
        }

        #endregion

        #region Methods



        #endregion
    }
}
