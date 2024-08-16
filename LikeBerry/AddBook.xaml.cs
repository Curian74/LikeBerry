using LikeBerry.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.IO;
using System.Text.RegularExpressions;

namespace LikeBerry
{
    public partial class AddBook : Window
    {
        LikeBerryContext context = new LikeBerryContext();
        User currentUser;
        private string selectedImagePath;
        private string destinationPath;

        public AddBook(User user)
        {
            InitializeComponent();
            currentUser = user;
            LoadNecessaryData();
        }

        public void LoadNecessaryData()
        {
            cmbAuthor.ItemsSource = context.Authors.ToList();
            cmbAuthor.SelectedIndex = 0;
            cmbGenre.ItemsSource = context.Genres.ToList();
            cmbGenre.SelectedIndex = 0;
        }

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                selectedImagePath = openFileDialog.FileName;
                txtImagePath.Text = System.IO.Path.GetFileName(selectedImagePath);
                imgPreview.Source = new BitmapImage(new Uri(selectedImagePath));
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBookName.Text)
                    || string.IsNullOrEmpty(txtISBN.Text)
                    || string.IsNullOrEmpty(txtQuantity.Text)
                    || string.IsNullOrEmpty(dpIssueDate.SelectedDate.ToString()))
                {
                    MessageBox.Show("You must input all fields!","Error",MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var checkDuplicateISBN = context.Books.FirstOrDefault(x => x.Isbn == txtISBN.Text);

                if( checkDuplicateISBN != null )
                {
                    MessageBox.Show("Duplicated ISBN code!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                if (!QuantityRegex(txtQuantity.Text))
                {
                    MessageBox.Show("You must input numeric value for quantity!", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (int.Parse(txtQuantity.Text) < 0)
                {
                    MessageBox.Show("Quantity must be positive!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (txtISBN.Text.Length != 13 || !ISBNRegex(txtISBN.Text))
                {
                    MessageBox.Show("ISBN code must be 13 digit characters value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Book book = new Book();
                book.BookName = txtBookName.Text;
                book.Isbn = txtISBN.Text;

                var author = cmbAuthor.SelectedItem as Author;

                if (author != null)
                {
                    book.AuthorId = author.AuthorId;
                }
                var genre = cmbGenre.SelectedItem as Genre;
                if (genre != null)
                {
                    book.GenreId = genre.GenreId;
                }
                book.InstockQuantity = int.Parse(txtQuantity.Text);

                var convertDate = DateOnly.Parse(dpIssueDate.Text);
                book.IssueDate = convertDate;

                // Handle image
                if (!string.IsNullOrEmpty(selectedImagePath))
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(selectedImagePath);
                    destinationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", fileName);

                    if (!File.Exists(destinationPath))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));
                        File.Copy(selectedImagePath, destinationPath, true);
                    }

                    book.Img = "Images/" + fileName;
                }
                else
                {
                    MessageBox.Show("Please select an image for the book.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var choice = MessageBox.Show("Are you sure you want to add the book?", "Confirmation", MessageBoxButton.OKCancel,
                    MessageBoxImage.Question);

                if (choice == MessageBoxResult.Cancel)
                {
                    return;
                }

                context.Add(book);
                context.SaveChanges();
                MessageBox.Show("Added new book successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ISBNRegex(string text)
        {
            return Regex.IsMatch(text, @"^\d{13}$");
        }

        private bool QuantityRegex(string text)
        {
            return Regex.IsMatch(text, @"^[0-9]+$");
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
