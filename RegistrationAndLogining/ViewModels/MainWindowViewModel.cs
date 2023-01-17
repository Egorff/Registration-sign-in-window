using PageSwitcherLib;
using RegistrationAndLogining.View.Pages;
using RegistrationAndLogining.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModelBaseLib.Command;
using ViewModelBaseLib.VM;
using static PageSwitcherLib.PageSwitcher;

namespace RegistrationAndLogining.ViewModels
{
    enum LoginRegistration : byte
    {
        register = 0, login
    }

    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields

        LoginRegistration m_loginRegistration;

        string m_loginButtonContent;

        string m_title;

        string m_message;

        object m_FrameContent;

        #endregion

        #region Prop

        public object FrameContent { get => m_FrameContent; set => Set(ref m_FrameContent, value); }

        public string Title { get => m_title; set => Set(ref m_title, value); }

        public string Message { get => m_message; set => Set(ref m_message, value); }

        public string LoginButtonContent { get => m_loginButtonContent; set => Set(ref m_loginButtonContent, value); }

        #endregion

        #region Ctor

        public MainWindowViewModel()
        {
            #region InitFields

            m_RegPage = new RegistrationPage();

            m_FrameContent = new object();

            m_LoginPage = new LoginPage();

            m_changePassPage = new ChangePassPage();

            RegisterPage(m_RegPage, "RgPg");

            RegisterPage(m_LoginPage, "LgPg");

            RegisterPage(m_changePassPage, "ChPg");

            PageSwitcher.OnPageChange += PageSwitcher_OnPageChange;

            ChangePage("RgPg");

            m_title = "Register";

            m_message = "If you already have an account, please sign in.";

            m_loginButtonContent = "Login";

            m_loginRegistration = LoginRegistration.register;

            #endregion

            #region Init commands

            OnLoginSwitchButtonPressed = new Command(OnLoginSwitchButtonPressedExecute, CanOnLoginSwitchButtonPressedExecute);

            #endregion
        }

        private void PageSwitcher_OnPageChange(Page obj)
        {
            FrameContent = obj;
        }

        #endregion

        #region Methods

        #region OnLoginSwitchButtonPressed

        bool CanOnLoginSwitchButtonPressedExecute(object p)
        {
            return true;
        }

        void OnLoginSwitchButtonPressedExecute(object p)
        {
            switch (m_loginRegistration)
            {
                case LoginRegistration.register:

                    Title = "Login";

                    Message = "If you don`t have an account, please register.";

                    LoginButtonContent = "Register";

                    ChangePage("LgPg");

                    m_loginRegistration = LoginRegistration.login;

                    break;

                case LoginRegistration.login:

                    ChangePage("RgPg");

                    Title = "Register";

                    Message = "If you already have an account, please sign in.";

                    LoginButtonContent = "Login";

                    m_loginRegistration = LoginRegistration.register;

                    break;
            }
        }

        #endregion

        #region OnCheckMailPageButtonPressed


        #endregion

        #endregion

        #region Pages

        RegistrationPage m_RegPage;

        LoginPage m_LoginPage;

        ChangePassPage m_changePassPage;

        #endregion

        #region Commands

        public ICommand OnLoginSwitchButtonPressed { get; set; }

        public ICommand OnCheckMailPageButtonPressed { get; set; }

        #endregion
    }
}
