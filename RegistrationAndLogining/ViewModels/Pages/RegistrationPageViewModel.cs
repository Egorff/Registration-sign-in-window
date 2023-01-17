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
using RegistrationAndLogining.View.Pages;
using System.Security.Cryptography.X509Certificates;
using CheckingEmailCode;
using System.Diagnostics.Eventing.Reader;
using System.Security.AccessControl;
using static Validation.Validaton;
using System.Windows.Controls;

namespace RegistrationAndLogining.ViewModels.Pages
{
    public class RegistrationPageViewModel : ViewModelBase
    {
        #region Fields

        CheckingEmailCodeClass m_checkingEmailCode;

        string m_login;

        string m_code;

        int[] m_intCode;

        DbController dbController;

        public string m_email;

        SecureString m_pass1;

        SecureString m_pass2;

        Visibility m_caseRegisterVisibility;

        Visibility m_caseCheckEmailVisibility;

        #endregion

        #region Events

        public event Action OnRegistrationFinished;

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

        public string Code { get => m_code; set => Set(ref m_code, value); }

        public Visibility CaseRegisterVisibility { get => m_caseRegisterVisibility; set => Set(ref m_caseRegisterVisibility, value); }

        public Visibility CaseCheckEmailVisibility { get => m_caseCheckEmailVisibility; set => Set(ref m_caseCheckEmailVisibility, value); }

        #endregion

        #region Ctor

        public RegistrationPageViewModel()
        {
            m_code = String.Empty;

            m_caseCheckEmailVisibility = Visibility.Hidden;

            m_caseRegisterVisibility = Visibility.Visible;

            m_login = string.Empty;

            m_checkingEmailCode = new CheckingEmailCodeClass(587, "smtp.gmail.com",  smtpDeliveryMethod:System.Net.Mail.SmtpDeliveryMethod.Network,
                new System.Net.Mail.MailAddress("Wrestler000ua@gmail.com"), true, false, new System.Net.NetworkCredential("wrestler000ua@gmail.com", "qilazsonycggwzlg"));

            dbController = new DbController();

            m_checkingEmailCode.OperationFinished += M_checkingEmailCode_OperationFinished;

            dbController.OperationFinished += DbController_OperationFinished; // Подписываемся на это событие

            m_email = string.Empty;

            m_ValidArray = new bool[6];

            #region Init commands

            OnRegisterButtonPressed = new Command(OnRegisterButtonPressedExecute, CanOnRegisterButtonPressedExecute);

            OnCheckCodeButtonPressed = new Command(OnCheckCodeButtonPressedExecute, CanOnCheckCodeButtonPressedExecute);

            OnWrongEmailButtonPressed = new Command(OnWrongEmailButtonPressedExecute, CanOnWrongEmailButtonPressedExecute);

            #endregion
        }

        private void M_checkingEmailCode_OperationFinished(ControllerBaseLib.OperationFinishedEventArgs<CheckingEmailOperations> obj)
        {
            if (obj.State == ControllerBaseLib.Enums.OperationState.OpSucceded)
            {
                CaseCheckEmailVisibility = Visibility.Visible;

                CaseRegisterVisibility = Visibility.Hidden;

                m_intCode = obj.OperationResult;
            }
            else if (obj.State == ControllerBaseLib.Enums.OperationState.OpFailed)
            {
                MessageBox.Show($"Fatal error! {obj.Exception.Message}");
            }
        }

        private void DbController_OperationFinished(ControllerBaseLib.OperationFinishedEventArgs<DatabaseOperations> obj)
        {
            if (obj.State == ControllerBaseLib.Enums.OperationState.OpSucceded)
            {
                switch (obj.Type)
                {
                    case DatabaseOperations.Register:

                        if (obj.OperationResult > 0)
                        {
                            Login = String.Empty;

                            Email = String.Empty;

                            m_pass1.Clear();

                            m_pass2.Clear();

                            m_pass1.Dispose();

                            m_pass2.Dispose();

                            CaseCheckEmailVisibility = Visibility.Hidden;

                            CaseRegisterVisibility = Visibility.Visible;

                            OnRegistrationFinished?.Invoke();

                            MessageBox.Show("Registration finished");
                        }
                        else
                        {
                            MessageBox.Show("Registration failed!", "Registration and logining", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        break;

                    case DatabaseOperations.Login:

                        break;
                }
            }
            else if (obj.State == ControllerBaseLib.Enums.OperationState.OpFailed)
            {
                MessageBox.Show($"Fatal error! {obj.Exception.Message}");
            }
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

                            m_ValidArray[1] = false;
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

                    case nameof(Code):

                        if (!CheckCode(m_intCode, Code))
                        {
                            error = "Wrong input.";

                            m_ValidArray[5] = false;
                        }
                        else
                        {
                            m_ValidArray[5] = true;
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
            return CheckValidArray(0, 4);
        }

        private void OnRegisterButtonPressedExecute(object p)
        {
            m_checkingEmailCode.CheckEmailCode(Email, true);
        }

        #endregion

        #region OnCheckCodeButtonPressed

        public bool CanOnCheckCodeButtonPressedExecute(object p)
        {
            return CheckValidArray(5, 6);
        }

        public void OnCheckCodeButtonPressedExecute(object p)
        {
            dbController.RegisterUser(Login, Email, m_pass2);
        }

        #endregion

        #region OnWrongEmailButtonPressed

        public bool CanOnWrongEmailButtonPressedExecute(object p)
        {
            return true;
        }

        public void OnWrongEmailButtonPressedExecute(object p)
        {
            CaseCheckEmailVisibility = Visibility.Hidden;

            CaseRegisterVisibility = Visibility.Visible;
        }

        #endregion

        #endregion

        #region Commands

        public ICommand OnCheckCodeButtonPressed { get; set; }

        public ICommand OnRegisterButtonPressed { get; set; }

        public ICommand OnWrongEmailButtonPressed { get; set; }

        #endregion
    }
}
