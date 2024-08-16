using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LikeBerry.Models;

namespace LikeBerry
{
    public partial class CustomerHomePage : Window
    {
        LikeBerryContext context = new LikeBerryContext();
        private User currentUser;
        private List<Book> allBooks;

        public CustomerHomePage(User u)
        {
            InitializeComponent();
            currentUser = u;
            LoadBookData();
        }

        public void LoadBookData()
        {
            allBooks = context.Books.Where(x => x.InstockQuantity > 0).ToList();
            bookList.ItemsSource = allBooks;

            var genres = context.Genres.ToList();
            genres.Insert(0, new Genre { GenreId = 0, GenreName = "All Genres" });
            genreFilter.ItemsSource = genres;
            genreFilter.SelectedIndex = 0;

            var authors = context.Authors.ToList();
            authors.Insert(0, new Author { AuthorId = 0, AuthorName = "All Authors" });
            authorFilter.ItemsSource = authors;
            authorFilter.SelectedIndex = 0;
        }

        private void FilterBooks(object sender, EventArgs e)
        {
            string searchText = searchBook.Text?.ToLower() ?? string.Empty;
            int selectedGenreId = 0;
            int selectedAuthorId = 0;

            if (genreFilter.SelectedItem is Genre selectedGenre)
            {
                selectedGenreId = selectedGenre.GenreId;
            }

            if (authorFilter.SelectedItem is Author selectedAuthor)
            {
                selectedAuthorId = selectedAuthor.AuthorId;
            }

            var filteredBooks = allBooks?.Where(book =>
                (book.BookName?.ToLower().Contains(searchText) ?? false) &&
                (selectedGenreId == 0 || book.GenreId == selectedGenreId) &&
                (selectedAuthorId == 0 || book.AuthorId == selectedAuthorId)
            ).ToList();

            bookList.ItemsSource = filteredBooks;
        }

        private void RefreshBooks_Click(object sender, RoutedEventArgs e)
        {
            LoadBookData();
            searchBook.Text = string.Empty;
            genreFilter.SelectedIndex = 0;
            authorFilter.SelectedIndex = 0;
        }

        private void BookNow_Click(object sender, RoutedEventArgs e)
        {
            if (bookList.SelectedItem is Book book)
            {
                var findBook = context.Books.FirstOrDefault(x => x.BookId == book.BookId);
                BookingProcessPage bookingProcessPage = new BookingProcessPage(findBook, currentUser);
                bookingProcessPage.Show();
                Close();
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            // Perform any necessary cleanup
            context.Dispose();

            // Create and show the login window
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();

            // Close the current window
            this.Close();
        }

        private void MyBooking_Click(object sender, RoutedEventArgs e)
        {
            MyBookingPage myBookingPage = new MyBookingPage(currentUser);
            myBookingPage.Show();

        }
    }
}