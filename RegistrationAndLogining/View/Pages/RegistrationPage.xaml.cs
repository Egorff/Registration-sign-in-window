using Accessibility;
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
using ViewModelBaseLib.Command;
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

            viewModel = new RegistrationPageViewModel();

            this.DataContext = viewModel;

            viewModel.OnRegistrationFinished += ViewModel_OnRegistrationFinished;

            if (!CheckPass(P1))
            {
                P1.BorderBrush = Brushes.OrangeRed;

                P1.BorderThickness = new Thickness(4);

                LabelPass1.Content = "Password mustn't be null";
            }

            if(!CheckPass(P2))
            {
                P2.BorderBrush = Brushes.OrangeRed;

                P2.BorderThickness = new Thickness(4);

                LabelPass2.Content = "Password mustn't be null";
            }

            //TextBlockYourEmail.Text = EmailTextBlock.Text;
        }

        private void ViewModel_OnRegistrationFinished()
        {
            P1.Clear();

            P2.Clear();
        }

        //private void P2_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if (CheckPass(P2) && CheckPass(P1))
        //    {
        //        viewModel.SetPass(this.P2.SecurePassword, 2);

        //        if (!viewModel.ComparePasswords())
        //        {
        //            ToolTip = "Passwords are not equale";

        //            P2.BorderBrush = Brushes.OrangeRed;

        //            P2.BorderThickness = new Thickness(4);
        //        }
        //        else
        //        {
        //            P2.ToolTip = "";

        //            P2.BorderBrush = Brushes.Green;

        //            P2.BorderThickness = new Thickness(2);
        //        }
        //    }
        //}

        bool CheckPass(PasswordBox pb)
        {
            if (pb.Password.Length == 0)
            {
                return false;
            }

            return true;
        }

        private void P2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (CheckPass(P2))
            {
                if (!viewModel.ComparePasswords(P2.SecurePassword))
                {
                    LabelPass2.Content = "Passwords are not equale";

                    P2.BorderBrush = Brushes.OrangeRed;

                    P2.BorderThickness = new Thickness(4);

                    viewModel.SetValidArray(3, false);
                }
                else
                {
                    LabelPass2.Content = "";

                    P2.BorderBrush = Brushes.Green;

                    P2.BorderThickness = new Thickness(2);

                    viewModel.SetPass(this.P2.SecurePassword, 2);

                    viewModel.SetValidArray(3, true);
                }
            }
            else
            {
                LabelPass2.Content = "Password mustn't be null";

                P2.BorderBrush = Brushes.OrangeRed;

                P2.BorderThickness = new Thickness(4);

                viewModel.SetValidArray(3, false);
            }
        }

        private void P1_LostFocus_1(object sender, RoutedEventArgs e)
        {
            if (CheckPass(P1))
            {
                viewModel.SetPass(this.P1.SecurePassword, 1);

                LabelPass1.Content = "";

                P1.BorderBrush = Brushes.Green;

                P1.BorderThickness = new Thickness(2);

                viewModel.SetValidArray(2, true);
            }
            else
            {
                LabelPass1.Content = "Password mustn't be null";

                P1.BorderBrush = Brushes.OrangeRed;

                P1.BorderThickness = new Thickness(4);

                viewModel.SetValidArray(2, false);
            }
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show("You have successfully registered.");
        //}
    }
}
