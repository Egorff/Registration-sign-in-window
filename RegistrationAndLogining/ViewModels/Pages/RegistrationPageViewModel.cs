using DatabaseControllerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBaseLib.VM;
using Validation;

namespace RegistrationAndLogining.ViewModels.Pages
{
    public class RegistrationPageViewModel : ViewModelBase
    {
        #region Fields

        string m_login;

        DbController dbController;

        string m_email;

        #endregion

        #region Prop

        public string Login 
        { 
            get => m_login; 

            set => Set(ref m_login, value); 
        }

        public string Email
        {
            get => m_email;

            set => Set(ref m_email, value);
        }

        #endregion

        #region Ctor

        public RegistrationPageViewModel()
        {
            m_login = string.Empty;

            dbController = new DbController();

            m_email = string.Empty;
        }

        #endregion

        #region IDataErrorInfo

        public override string this[string columnName]
        {
            get 
            { 
                string error = string.Empty;

                switch (columnName)
                {
                    case nameof(Login):
                        if (string.IsNullOrEmpty(Login))
                        {
                            error = "Field is mustn't be null.";
                        }

                        if (dbController.IsLoginExists(Login))
                        {
                            error = "This login is alredy exists!";
                        }

                        break;

                    case nameof(Email):
                        if (!string.IsNullOrEmpty(Email))
                        {
                            if (!Validaton.ValidateEmail(Email))
                            {
                                error = "E-mail is wrong!";
                            }

                            if (dbController.IsEmailExists(Email))
                            {
                                error = "This E-mail is alredy exists!";
                            }
                        }
                        else
                        {
                            error = "Field is mustn't be null.";
                        }

                        break;
                }

                return error;
            }
        }

        #endregion

        #region Methods



        #endregion
    }
}
