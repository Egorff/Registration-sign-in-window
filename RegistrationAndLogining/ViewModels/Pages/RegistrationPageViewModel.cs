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
using System.Windows.Input;
using ViewModelBaseLib.Command;

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

            m_ValidArray = new bool[4];

            #region Init commands

            OnRegisterButtonPressed = new Command(OnRegisterButtonPressedExecute, CanOnRegisterButtonPressedExecute);

            #endregion
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

                            m_ValidArray[0] = false;
                        }
                        else
                        {
                            if (dbController.IsLoginExists(Login))
                            {
                                error = "This login is alredy exists!";

                                m_ValidArray[0] = false;
                            }
                            else
                            {
                                m_ValidArray[0] = true;
                            }
                        }

                        break;

                    case nameof(Email):
                        if (string.IsNullOrEmpty(Email))
                        {
                            error = "Field is mustn't be null.";
                        }
                        else
                        {
                            if (!Validaton.ValidateEmail(Email))
                            {
                                error = "E-mail is wrong!";

                                m_ValidArray[1] = false;
                            }
                            else
                            {
                                if (dbController.IsEmailExists(Email))
                                {
                                    error = "This E-mail is alredy exists!";

                                    m_ValidArray[1] = false;
                                }
                                else
                                {
                                    m_ValidArray[1] = true;
                                }
                            }
                        }

                        break;
                }

                return error;
            }
        }

        #endregion

        #region Methods
        
        public void SetValidArray(int index, bool value)
        {
            m_ValidArray[index] = value;
        }

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

        public bool ComparePasswords(SecureString value)
        {
            return m_pass1.Compare(value);
        }

        #region OnRegisterButtonPressed

        private bool CanOnRegisterButtonPressedExecute(object p)
        {
            return CheckValidArray(0, 2);
        }

        private void OnRegisterButtonPressedExecute(object p)
        {
            
        }

        #endregion

        #endregion

        #region Commands

        public ICommand OnRegisterButtonPressed { get; }

        #endregion
    }
}
