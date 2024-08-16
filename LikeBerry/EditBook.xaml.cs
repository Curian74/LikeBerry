using LikeBerry.Models;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media.Imaging;

namespace LikeBerry
{
    public partial class EditBook : Window
    {
        LikeBerryContext context = new LikeBerryContext();
        private Book _book;
        private User currentUser;
        private string selectedImagePath;

        public EditBook(Book book, User user)
        {
            InitializeComponent();
            currentUser = user;
            _book = book;
            LoadBookData();
        }

        private void LoadBookData()
        {
            var findBook = context.Books.FirstOrDefault(a => a.BookId == _book.BookId);
            cmbAuthor.ItemsSource = context.Authors.ToList();
            cmbGenre.ItemsSource = context.Genres.ToList();
            txtBookName.Text = findBook.BookName;
            txtISBN.Text = findBook.Isbn;
            txtQuantity.Text = findBook.InstockQuantity.ToString();
            dpIssueDate.SelectedDate = DateTime.Parse(findBook.IssueDate.ToString());

            var author = context.Authors.FirstOrDefault(a => a.AuthorId == findBook.AuthorId);
            if (author != null)
            {
                cmbAuthor.SelectedItem = author;
            }

            var genre = context.Genres.FirstOrDefault(g => g.GenreId == findBook.GenreId);
            if (genre != null)
            {
                cmbGenre.SelectedItem = genre;
            }

            // Load and display the current image
            if (!string.IsNullOrEmpty(findBook.Img))
            {
                txtImagePath.Text = System.IO.Path.GetFileName(findBook.Img);
                selectedImagePath = findBook.Img;
            }
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

        private void SaveBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var findBook = context.Books.FirstOrDefault(a => a.BookId == _book.BookId);
                findBook.BookName = txtBookName.Text;
                findBook.Isbn = txtISBN.Text;

                if (string.IsNullOrEmpty(txtBookName.Text)
                   || string.IsNullOrEmpty(txtISBN.Text)
                   || string.IsNullOrEmpty(txtQuantity.Text)
                   || string.IsNullOrEmpty(dpIssueDate.SelectedDate.ToString()))
                {
                    MessageBox.Show("You must input all fields!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var checkDuplicateISBN = context.Books.Where(x => x.BookId != findBook.BookId).FirstOrDefault(x => x.Isbn == txtISBN.Text);

                if (checkDuplicateISBN != null)
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

                if (!ISBNRegex(txtISBN.Text))
                {
                    MessageBox.Show("ISBN code must be 13 digit characters value!",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                findBook.InstockQuantity = int.Parse(txtQuantity.Text);

                if (dpIssueDate.SelectedDate.HasValue)
                {
                    findBook.IssueDate = DateOnly.FromDateTime(dpIssueDate.SelectedDate.Value);
                }

                var selectedAuthor = cmbAuthor.SelectedItem as Author;
                if (selectedAuthor != null)
                {
                    findBook.AuthorId = selectedAuthor.AuthorId;
                }

                var selectedGenre = cmbGenre.SelectedItem as Genre;
                if (selectedGenre != null)
                {
                    findBook.GenreId = selectedGenre.GenreId;
                }

                // Handle image update
                if (!string.IsNullOrEmpty(selectedImagePath) && selectedImagePath != findBook.Img)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(selectedImagePath);
                    string destinationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", fileName);

                    if (!File.Exists(destinationPath))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));
                        File.Copy(selectedImagePath, destinationPath, true);

                        // Delete the old image file if it exists and is not a web URL
                        if (!string.IsNullOrEmpty(findBook.Img) && !findBook.Img.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                        {
                            string oldImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, findBook.Img);
                            if (File.Exists(oldImagePath))
                            {
                                File.Delete(oldImagePath);
                            }
                        }
                    }

                    findBook.Img = "Images/" + fileName;
                }

                var choice = MessageBox.Show("Are you sure you want to update?", "Confirmation", MessageBoxButton.OKCancel,
                    MessageBoxImage.Question);

                if (choice == MessageBoxResult.Cancel)
                {
                    return;
                }

                context.Update(findBook);
                context.SaveChanges();
                MessageBox.Show("Book details updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool QuantityRegex(string text)
        {
            return Regex.IsMatch(text, @"^[0-9]+$");
        }

        private bool ISBNRegex(string text)
        {
            return Regex.IsMatch(text, @"^\d{13}$");
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}