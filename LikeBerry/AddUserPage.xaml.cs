using LikeBerry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Window
    {
        LikeBerryContext context = new LikeBerryContext();
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        string phonePattern = @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$";
        private User currentUser;

        public AddUserPage(User user)
        {
            currentUser = user;
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            cmbRole.ItemsSource = context.Roles.ToList();
            cmbRole.SelectedIndex = 0;
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFullName.Text)
                    || string.IsNullOrEmpty(txtEmail.Text)
                    || string.IsNullOrEmpty(txtPassword.Password)
                    || string.IsNullOrEmpty(txtPhoneNumber.Text)
                    || string.IsNullOrEmpty(txtAddress.Text))
                {
                    MessageBox.Show("You must input all fields!", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                if (!Regex.IsMatch(txtEmail.Text, emailPattern))
                {
                    MessageBox.Show("Please enter a valid email address.", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                
                if (!Regex.IsMatch(txtPhoneNumber.Text, phonePattern) || txtPhoneNumber.Text.Length < 10)
                {
                    MessageBox.Show("Please enter a valid phone number.", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var findUserEmail = context.Users.FirstOrDefault(x => x.Email == txtEmail.Text);
                if (findUserEmail != null)
                {
                    MessageBox.Show("The email is already taken", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var choice = MessageBox.Show("Are you sure you want to add the new user?",
                    "Confirmation", MessageBoxButton.OKCancel,
                    MessageBoxImage.Question);

                if (choice == MessageBoxResult.Cancel)
                {
                    return;
                }

                User user = new User();
                user.FullName = txtFullName.Text;
                user.Email = txtEmail.Text;
                user.Password = txtPassword.Password;
                var role = cmbRole.SelectedItem as Role;
                if (role != null)
                {
                    user.RoleId = role.RoleId;
                }
                user.Status = chkStatus.IsChecked ?? false;
                user.PhoneNumber = txtPhoneNumber.Text;
                user.Address = txtAddress.Text;
                context.Add(user);
                context.SaveChanges();
                MessageBox.Show("Added new user successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
