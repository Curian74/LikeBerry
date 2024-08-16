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
    /// Interaction logic for AddAuthorPage.xaml
    /// </summary>
    public partial class AddAuthorPage : Window
    {
        LikeBerryContext context = new LikeBerryContext();
        User currentUser;
        public AddAuthorPage(User user)
        {
            currentUser = user;
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Author a = new Author();
            a.AuthorName = txtAuthorName.Text;

            if (string.IsNullOrEmpty(txtAuthorName.Text))
            {
                MessageBox.Show("Please input author name!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }

            var choice = MessageBox.Show("Are you sure you want to add the author?", "Confirmation", MessageBoxButton.OKCancel,
                    MessageBoxImage.Question);

            if (choice == MessageBoxResult.Cancel)
            {
                return;
            }

            context.Authors.Add(a);
            context.SaveChanges();
            MessageBox.Show("Added new author successfully!", "Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
