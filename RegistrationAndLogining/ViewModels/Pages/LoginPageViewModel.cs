using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecureStringExtentionsLib;
using System.Windows.Input;
using ViewModelBaseLib.Command;
using RegistrationAndLogining.View.Pages;
using ViewModelBaseLib.VM;
using DatabaseControllerLib;
using System.Security;
using System.Windows;
using System.Data.Entity.Core;

namespace RegistrationAndLogining.ViewModels.Pages
{
    public class LoginPageViewModel : ViewModelBase
    {
        #region Fields

        string m_login;

        SecureString m_password;

        DbController m_dbController;

        #endregion

        #region Prop

        public string Login { get => m_login; set => Set(ref m_login, value); }

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

                        if (string.IsNullOrWhiteSpace(Login))
                        {
                            error = "This field is mustn't be null.";

                            m_ValidArray[0] = false;
                        }
                        else
                        {
                            m_ValidArray[0] = true;
                        }

                        break;
                }

                return error;
            }
        }

        #endregion

        #region Event

        public event Action OnLoginingFinished;

        #endregion

        #region Ctor

        public LoginPageViewModel()
        {
            #region intt fields

            m_login = String.Empty;

            m_ValidArray = new bool[2];

            m_dbController = new DbController();

            m_dbController.OperationFinished += M_dbController_OperationFinished;

            #endregion

            #region Init commands

            OnSignInButtonPressed = new Command(OnSignInButtonPressedExecute, CanOnSignInButtonPressedExecute);

            #endregion
        }

        private void M_dbController_OperationFinished(ControllerBaseLib.OperationFinishedEventArgs<DatabaseOperations> obj)
        {
            if (obj.State == ControllerBaseLib.Enums.OperationState.OpSucceded)
            {
                switch (obj.Type)
                {
                    case DatabaseOperations.Login:

                        if (obj.OperationResult != null)
                        {
                            Login = String.Empty;

                            m_password.Clear();

                            m_password.Dispose();

                            OnLoginingFinished?.Invoke();

                            MessageBox.Show($"Logining finished {obj.OperationResult}", "Logining and registrationing", 
                                MessageBoxButton.OK, MessageBoxImage.Information);

                            
                        }
                        else
                        {
                            MessageBox.Show("Wrong login or password", "Registration and logining", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        break;
                }
            }
            else if (obj.State == ControllerBaseLib.Enums.OperationState.OpFailed)
            {
                if (obj.Exception is InvalidOperationException)
                {
                    MessageBox.Show("Wrong login or password", "Registration and logining", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show($"Fatal error! {obj.Exception.Message}");
                }
            }
        }

        #endregion

        #region Methods

        public void SetPass(SecureString value)
        {
            m_password = value;
        }

        public void SetValidArray(int index, bool value)
        {
            m_ValidArray[index] = value;
        }

        #region OnSignInButtonPressed

        public bool CanOnSignInButtonPressedExecute(object p)
        {
            return CheckValidArray(0, m_ValidArray.Length);
        }

        public void OnSignInButtonPressedExecute(object p)
        {
            m_dbController.LoginUser(Login, m_password);
        }

        #endregion

        #endregion

        #region Commands

        public ICommand OnSignInButtonPressed { get; }

        #endregion
    }
}
