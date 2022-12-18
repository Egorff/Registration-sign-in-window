using RegistrationAndLogining.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
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
using ViewModelBaseLib.VM;

namespace RegistrationAndLogining.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        RegistrationPageViewModel viewModel;

        public RegistrationPage()
        {
            InitializeComponent();

            //if (!CheckPass(P1))
            //{
            //    LabelPass1.Content = "This field is mustn't be null";

            //    P1.BorderBrush = Brushes.OrangeRed;

            //    P1.BorderThickness = new Thickness(4);
            //}

            //if (!CheckPass(P2))
            //{
            //    LabelPass2.Content = "This field is mustn't be null";

            //    P2.BorderBrush = Brushes.OrangeRed;

            //    P2.BorderThickness = new Thickness(4);
            //}

            viewModel = new RegistrationPageViewModel();

            this.DataContext = viewModel;
        }

        private void P1_LostFocus(object sender, RoutedEventArgs e)
        {
            viewModel.SetPass(this.P1.SecurePassword, 1);
        }

        private void P2_LostFocus(object sender, RoutedEventArgs e)
        {
            viewModel.SetPass(this.P2.SecurePassword, 2);

           viewModel.ComparePasswords();
        }

        bool CheckPass(PasswordBox pb)
        {
            return pb.Password.Length > 0;
        }
    }
}
