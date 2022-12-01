using RegistrationAndLogining.View.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBaseLib.VM;

namespace RegistrationAndLogining.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields

        object m_FrameContent;

        #endregion
        


        #region Prop

        public object FrameContent { get => m_FrameContent; set => Set(ref m_FrameContent, value); }

        #endregion



        #region Ctor

        public MainWindowViewModel()
        {
            #region InitFields

            m_RegPage = new RegistrationPage();

            m_LoginPage = new LoginPage();

            m_FrameContent = new object();

            m_FrameContent = m_RegPage;

            #endregion
        }

        #endregion



        #region Methods

        #endregion



        #region Pages

        RegistrationPage m_RegPage;

        LoginPage m_LoginPage;

        #endregion
    }
}
