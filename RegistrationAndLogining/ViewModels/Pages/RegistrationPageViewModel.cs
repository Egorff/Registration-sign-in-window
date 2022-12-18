using DatabaseControllerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBaseLib.VM;
using Validation;
using System.Security;
using System.Windows;
using SecureStringExtentionsLib;

namespace RegistrationAndLogining.ViewModels.Pages
{
    public class RegistrationPageViewModel : ViewModelBase
    {
        #region Fields

        string m_login;

        DbController dbController;

        string m_email;

        SecureString m_pass1;

        SecureString m_pass2;

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
        
        /// <summary>
        /// Set password field according to the number
        /// number is from 1 to 2
        /// </summary>
        /// <param name="value"></param>
        /// <param name="number"></param>
        public void SetPass(SecureString value, byte number)
        {
            if (number == 1)
            {
                m_pass1 = value;
            }
            else
            {
                m_pass2 = value;
            }
        }

        public void ComparePasswords()
        {
            if (m_pass1.Compare(m_pass2))
            {
                MessageBox.Show("Passwords are equale");
            }
            else
            {
                MessageBox.Show("Passwords aren't equale");
            }
        }

        #endregion
    }
}
