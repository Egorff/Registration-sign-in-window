using DatabaseControllerLib;
using RegistrationAndLogining.ViewModels;
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

namespace RegistrationAndLogining.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChangePassPage.xaml
    /// </summary>
    public partial class ChangePassPage : Page
    {
        ChangePassPageViewModel m_changePassPageVeiwModel;

        public ChangePassPage()
        {
            InitializeComponent();

            m_changePassPageVeiwModel = new ChangePassPageViewModel();

            this.DataContext = m_changePassPageVeiwModel;

            if (!CheckPass(NewPass))
            {
                NewPass.BorderBrush = Brushes.OrangeRed;

                NewPass.BorderThickness = new Thickness(4);

                LabelPass2.Content = "Password mustn't be null";
            }
        }

        private void NewPass_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CheckPass(NewPass))
            {
                m_changePassPageVeiwModel.SetPass(this.NewPass.SecurePassword);

                LabelPass2.Content = "";

                NewPass.BorderBrush = Brushes.Green;

                NewPass.BorderThickness = new Thickness(2);

                m_changePassPageVeiwModel.SetValidArray(0, true);
            }
            else
            {
                LabelPass2.Content = "Password mustn't be null";

                NewPass.BorderBrush = Brushes.OrangeRed;

                NewPass.BorderThickness = new Thickness(4);

                m_changePassPageVeiwModel.SetValidArray(0, false);
            }
        }

        public bool CheckPass(PasswordBox pb)
        {
            if (pb.Password.Length == 0)
            {
                return false;
            }

            return true;
        }
    }
}
