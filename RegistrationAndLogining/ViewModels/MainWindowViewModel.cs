using RegistrationAndLogining.View.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModelBaseLib.Command;
using ViewModelBaseLib.VM;

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

            m_LoginPage = new LoginPage();

            m_FrameContent = new object();

            m_FrameContent = m_RegPage;

            m_title = "Register";

            m_message = "If you already have an account, please sign in.";

            m_loginButtonContent = "Login";

            m_loginRegistration = LoginRegistration.register;

            #endregion

            #region Init commands

            OnLoginSwitchButtonPressed = new Command(OnLoginSwitchButtonPressedExecute, CanOnLoginSwitchButtonPressedExecute);

            #endregion
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

                    FrameContent = m_LoginPage;

                    m_loginRegistration = LoginRegistration.login;

                    break;

                case LoginRegistration.login:

                    FrameContent = m_RegPage;

                    Title = "Register";

                    Message = "If you already have an account, please sign in.";

                    LoginButtonContent = "Login";

                    m_loginRegistration = LoginRegistration.register;

                    break;
            }
        }

        #endregion

        #endregion



        #region Pages

        RegistrationPage m_RegPage;

        LoginPage m_LoginPage;

        #endregion



        #region Commands

        public ICommand OnLoginSwitchButtonPressed { get; set; }

        #endregion
    }
}
