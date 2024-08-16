using LikeBerry.Models;
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
using System.Windows.Shapes;

namespace LikeBerry
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        LikeBerryContext context = new LikeBerryContext();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        public void Login()
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter email and password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                User user = context.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
                if (user != null)
                {
                    if (user.Status == false)
                    {
                        MessageBox.Show("Account is suspended", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    if (user.RoleId == 1 || user.RoleId == 2)
                    {
                        MainWindow mainWindow = new MainWindow(user);
                        mainWindow.Show();
                    }

                    else if(user.RoleId == 3)
                    {
                        CustomerHomePage customerHomePage = new CustomerHomePage(user);
                        customerHomePage.Show();
                    }
                    
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid email or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Login();
            }
        }
    }
}
