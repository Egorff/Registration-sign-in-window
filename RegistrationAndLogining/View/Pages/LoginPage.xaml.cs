using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Identity.Client;
using RegistrationAndLogining.ViewModels.Pages;
using ViewModelBaseLib.VM;

namespace RegistrationAndLogining.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        #region Fields

        LoginPageViewModel m_loginPageViewModel;

        #endregion

        public LoginPage()
        {
            InitializeComponent();

            m_loginPageViewModel = new LoginPageViewModel();

            this.DataContext = m_loginPageViewModel;

            if (!CheckPass(P1))
            {
                LabelPass1.Content = "Password mustn`t be null.";

                P1.BorderBrush = Brushes.OrangeRed;

                P1.BorderThickness = new Thickness(4);
            }
        }

        #region Methods

        public bool CheckPass(PasswordBox pb)
        {
            if (pb.Password.Length == 0)
            {
                return false;
            }

            return true;
        }

        #endregion

        private void Grid_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CheckPass(P1))
            {
                m_loginPageViewModel.SetPass(this.P1.SecurePassword, 1);

                LabelPass1.Content = "";

                P1.BorderBrush = Brushes.Green;

                P1.BorderThickness = new Thickness(2);

                m_loginPageViewModel.SetValidArray(4, true);
            }
            else
            {
                LabelPass1.Content = "Password mustn't be null";

                P1.BorderBrush = Brushes.OrangeRed;

                P1.BorderThickness = new Thickness(4);

                m_loginPageViewModel.SetValidArray(4,false);
            }
        }
    }
}
