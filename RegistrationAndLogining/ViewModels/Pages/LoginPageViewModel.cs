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
using static Validation.Validaton;
using CheckingEmailCode;
using System.Windows.Controls;
using static PageSwitcherLib.PageSwitcher;

namespace RegistrationAndLogining.ViewModels.Pages
{
    public class LoginPageViewModel : ViewModelBase
    {
        #region Fields

        string m_login;

        string m_email;

        SecureString m_password;

        DbController m_dbController;

        Visibility m_caseForgotPassGrid;

        Visibility m_caseLoginGrid;

        Visibility m_caseInputCode;

        string m_code;

        CheckingEmailCodeClass m_checkingEmailCode;

        int[] m_intCode;

        #endregion

        #region Prop

        public string Login { get => m_login; set => Set(ref m_login, value); }

        public string Code { get => m_code; set => Set(ref m_code, value); }

        public Visibility ForgotPassGridVisibility { get => m_caseForgotPassGrid; set => Set(ref m_caseForgotPassGrid, value); }

        public Visibility LoginVisibility { get => m_caseLoginGrid; set => Set(ref m_caseLoginGrid, value); }

        public Visibility InputCodeVisibility { get => m_caseInputCode; set => Set(ref m_caseInputCode, value); }

        public string Email { get => m_email; set => Set(ref m_email, value); }

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

                        case nameof(Email):

                        if (string.IsNullOrWhiteSpace(Email))
                        {
                            error = "Field is mustn't be null.";

                            m_ValidArray[2] = false;
                        }
                        else
                        {
                            if (!Validation.Validaton.ValidateEmail(Email))
                            {
                                error = "Wrong email.";

                                m_ValidArray[2] = false;
                            }
                            else
                            {
                                if (m_dbController.IsEmailExists(Email))
                                {
                                    error = "";

                                    m_ValidArray[2] = true;
                                }
                                else
                                {
                                    error = "Email dosen`t exeist.";

                                    m_ValidArray[2] = false;
                                }
                            }
                        }

                        break;

                    case nameof(Code):

                        if (!ValidateCode(Code, out error))
                        {
                            m_ValidArray[4] = false;
                        }
                        else
                        {
                            m_ValidArray[4] = true;
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

            m_checkingEmailCode = new CheckingEmailCodeClass(587, "smtp.gmail.com", smtpDeliveryMethod: System.Net.Mail.SmtpDeliveryMethod.Network,
                new System.Net.Mail.MailAddress("Wrestler000ua@gmail.com"), true, false, new System.Net.NetworkCredential("wrestler000ua@gmail.com", "qilazsonycggwzlg"));

            m_login = String.Empty;

            m_email = String.Empty;

            m_code = String.Empty;

            m_ValidArray = new bool[5];

            m_dbController = new DbController();

            m_dbController.OperationFinished += M_dbController_OperationFinished;

            m_checkingEmailCode.OperationFinished += M_checkingEmailCode_OperationFinished;

            m_caseForgotPassGrid = Visibility.Hidden;

            m_caseLoginGrid = Visibility.Visible;

            m_caseInputCode = Visibility.Hidden;

            #endregion

            #region Init commands

            OnSignInButtonPressed = new Command(OnSignInButtonPressedExecute, CanOnSignInButtonPressedExecute);

            OnForgotPassButtonPressed = new Command(OnForgotPassButtonPressedExecute, CanOnForgotPassButtonPressedExecute);

            OnBackToLoginButtonPressed = new Command(OnBackToLoginButtonPressedExecute, CanOnBackToLoginButtonPressedExecute);

            OnInputedWronEmailButtonPressed = new Command(OnInputedWronEmailButtonPressedExecute, CanOnInputedWronEmailButtonPressedExecute);

            OnCheckEmailButtonPressed = new Command (OnCheckEmailButtonPressedExecute, CanOnCheckEmailButtonPressedExecute);

            OnCheckCodeButtonPressed = new Command(OnCheckCodeButtonPressedExecute, CanOnCheckCodeButtonPressedExecute);

            #endregion
        }

        private void M_checkingEmailCode_OperationFinished(ControllerBaseLib.OperationFinishedEventArgs<CheckingEmailOperations> obj)
        {
            if (obj.State == ControllerBaseLib.Enums.OperationState.OpSucceded)
            {
                LoginVisibility = Visibility.Hidden;

                ForgotPassGridVisibility = Visibility.Hidden;

                m_intCode = obj.OperationResult;
            }
            else if (obj.State == ControllerBaseLib.Enums.OperationState.OpFailed)
            {
                MessageBox.Show($"Fatal error! {obj.Exception.Message}");
            }
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

        public void SetPass(SecureString value, byte number)
        {
           m_password = value;
        }

        public void SetValidArray(int index, bool value)
        {
            m_ValidArray[index] = value;
        }

        #region OnForgotPassButtonPressed

        public bool CanOnForgotPassButtonPressedExecute(object p)
        {
            return true;
        }

        public void OnForgotPassButtonPressedExecute(object p)
        {
            ForgotPassGridVisibility = Visibility.Visible;

            LoginVisibility = Visibility.Hidden;
        }

        #endregion

        #region OnSignInButtonPressed

        public bool CanOnSignInButtonPressedExecute(object p)
        {
            return CheckValidArray(0, 1);
        }

        public void OnSignInButtonPressedExecute(object p)
        {
            m_dbController.LoginUser(Login, m_password);
        }

        #endregion

        #region OnBackToLoginButtonPressed

        public bool CanOnBackToLoginButtonPressedExecute(object p)
        {
            return true;
        }

        public void OnBackToLoginButtonPressedExecute(object p)
        {
            LoginVisibility = Visibility.Visible;

            ForgotPassGridVisibility = Visibility.Hidden;
        }

        #endregion

        #region OnCheckEmailButtonPressed

        public bool CanOnCheckEmailButtonPressedExecute(object p)
        {
            return CheckValidArray(2,3);
        }

        public void OnCheckEmailButtonPressedExecute(object p)
        {
            m_checkingEmailCode.CheckEmailCode(Email, true);

            ForgotPassGridVisibility = Visibility.Hidden;

            InputCodeVisibility = Visibility.Visible;
        }

        #endregion

        #region OnInputedWronEmailButtonPressed

        public bool CanOnInputedWronEmailButtonPressedExecute(object p)
        {
            return true;
        }

        public void OnInputedWronEmailButtonPressedExecute(object p)
        {
            LoginVisibility = Visibility.Hidden;

            ForgotPassGridVisibility = Visibility.Visible;

            InputCodeVisibility = Visibility.Hidden;
        }

        #endregion

        #region OnCheckCodeButtonPressed

        public bool CanOnCheckCodeButtonPressedExecute(object p)
        {
            return CheckValidArray(4, 5);
        }

        public void OnCheckCodeButtonPressedExecute(object p)
        {
            if (CheckCode(m_intCode, Code))
            {
                ChangePage("ChPg");
            }
            else
            {
                MessageBox.Show("Wrong password.");
            }
        }

        #endregion

        #endregion

        #region Commands

        public ICommand OnSignInButtonPressed { get; }

        public ICommand OnForgotPassButtonPressed { get; }

        public ICommand OnBackToLoginButtonPressed { get; }
        
        public ICommand OnCheckEmailButtonPressed { get; }

        public ICommand OnInputedWronEmailButtonPressed { get; }

        public ICommand OnCheckCodeButtonPressed { get; }

        #endregion
    }
}
