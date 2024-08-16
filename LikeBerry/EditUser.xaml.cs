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
    /// Interaction logic for EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        LikeBerryContext context = new LikeBerryContext();
        private User user;
        private User currentUser;

        public EditUser(User targetUser, User sentUser)
        {
            InitializeComponent();
            user = targetUser;
            currentUser = sentUser;
            LoadData();
        }

        public void LoadData()
        {
            cmbRole.ItemsSource = context.Roles.ToList();
            txtFullName.Text = user.FullName;
            txtEmail.Text = user.Email;
            txtAddress.Text = user.Address;
            txtPhoneNumber.Text = user.PhoneNumber;

            var role = context.Roles.FirstOrDefault(a => a.RoleId == user.RoleId);
            if (role != null)
            {
                cmbRole.SelectedItem = role;
            }

            if(user.Status == true)
            {
                chkStatus.IsChecked = true;
            }

            else
            {
                chkStatus.IsChecked = false;
            }

            if(currentUser.UserId == user.UserId)
            {
                warn.Text = "* You cannot edit your own profile role and status! *";
                cmbRole.IsEnabled = false;
                chkStatus.IsEnabled = false;
            }

        }

        private void SaveUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var findUser = context.Users.FirstOrDefault(u => u.UserId == user.UserId);
                if (findUser == null)
                {
                    MessageBox.Show("User not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                findUser.FullName = txtFullName.Text;
                findUser.Email = txtEmail.Text;
                findUser.Address = txtAddress.Text;
                findUser.PhoneNumber = txtPhoneNumber.Text;

                var selectedRole = cmbRole.SelectedItem as Role;
                if (selectedRole != null)
                {
                    findUser.RoleId = selectedRole.RoleId;
                }

                findUser.Status = chkStatus.IsChecked ?? false;

                var choice = MessageBox.Show("Are you sure you want to edit this user?",
                    "Confirmation", MessageBoxButton.OKCancel,
                    MessageBoxImage.Question);

                if (choice == MessageBoxResult.Cancel)
                {
                    return;
                }

                context.Update(findUser);
                context.SaveChanges();

                MessageBox.Show("User details updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelEdit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
