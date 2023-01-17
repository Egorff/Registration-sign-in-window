using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModelBaseLib.Command;
using RegistrationAndLogining.View.Pages;
using ViewModelBaseLib.VM;
using DatabaseControllerLib;
using System.Security;
using System.Windows;
using System.Data.Entity.Core;
using Validation;
using CheckingEmailCode;
using static PageSwitcherLib.PageSwitcher;

namespace RegistrationAndLogining.ViewModels.Pages
{
    public class ChangePassPageViewModel : ViewModelBase
    {
        #region Fields

        SecureString m_newPassword;

        DbController m_dbController;

        #endregion

        #region Prop

        #endregion

        #region Ctor

        public ChangePassPageViewModel()
        {
            m_ValidArray = new bool[1];

            m_dbController = new DbController();

            OnPasswordChangeButtonPressed = new Command(OnPasswordChangeButtonPressedExecute, CanOnPasswordChangeButtonPressedExecute);
        }

        #endregion

        #region Methods
        public void SetValidArray(int index, bool value)
        {
            m_ValidArray[index] = value;
        }
        public void SetPass(SecureString value)
        {
            m_newPassword = value;
        }

        #region OnPasswordChangeButtonPressed

        public bool CanOnPasswordChangeButtonPressedExecute(object p)
        {
            return CheckValidArray(0, 1);
        }

        public void OnPasswordChangeButtonPressedExecute(object p)
        {
            // m_dbController.ChangePassword(m_newPassword, m_loginPageViewModel.Email);

            ChangePage("LgPg");

            MessageBox.Show("You successfuly changes your password.");
        }

        #endregion

        #endregion

        #region Commands

        public ICommand OnPasswordChangeButtonPressed { get; set; }

        #endregion
    }
}
