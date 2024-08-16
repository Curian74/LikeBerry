using LikeBerry.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LikeBerry
{
    public partial class MainWindow : Window
    {
        private User currentUser;
        LikeBerryContext context = new LikeBerryContext();

        private List<Book> allBooks;
        private List<User> allUsers;
        private List<Booking> allBookings;
        private List<Booking> allFinishedBookings;


        public MainWindow(User user)
        {
            InitializeComponent();
            currentUser = user;
            SetTabVisibility();
            LoadBookData();
            LoadUserData();
            LoadBookingData();
            LoadFinishedBookingData();
        }

        private void SetTabVisibility()
        {
            if (currentUser.RoleId == 2)
            {
                usersTab.Visibility = Visibility.Collapsed;
            }
        }

        public void LoadFinishedBookingData()
        {
            try
            {

                allFinishedBookings = context.Bookings
                    .Include(x => x.User)
                    .Include(x => x.BookingDetails)
                    .Include(x => x.ProcessedByNavigation)
                    .Where(x => x.IsFinished == true)
                    .OrderByDescending(x => x.BookingDate)
                    .ToList();
                finishedBookingList.ItemsSource = allFinishedBookings;

                finishedStatusFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading finished booking data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchFinishedBookingTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterFinishedBookings();
        }

        private void FilterFinishedBookings(object sender = null, SelectionChangedEventArgs e = null)
        {
            string searchText = searchFinishedBooking.Text?.Trim() ?? string.Empty;
            string selectedStatus = ((ComboBoxItem)finishedStatusFilter.SelectedItem)?.Content as string ?? "All Statuses";

            var filteredBookings = allFinishedBookings.Where(booking =>
                (string.IsNullOrEmpty(searchText) || booking.User.UserId.ToString().Contains(searchText)) &&
                (selectedStatus == "All Statuses" || booking.Status == selectedStatus)
            ).ToList();

            finishedBookingList.ItemsSource = filteredBookings;
        }

        private void RefreshFinishedBookings_Click(object sender, RoutedEventArgs e)
        {
            allFinishedBookings.Clear();
            finishedBookingList.ItemsSource = null;
            finishedStatusFilter.SelectedIndex = 0;
            searchFinishedBooking.Text = string.Empty;

            context = new LikeBerryContext();

            LoadFinishedBookingData();

            this.UpdateLayout();
        }

        public void LoadBookData()
        {
            try
            {
                var genres = context.Genres.ToList();
                genres.Insert(0, new Genre { GenreId = 0, GenreName = "All Genres" });
                genreFilter.ItemsSource = genres;
                genreFilter.SelectedIndex = 0;

                var authors = context.Authors.ToList();
                authors.Insert(0, new Author { AuthorId = 0, AuthorName = "All Authors" });
                authorFilter.ItemsSource = authors;
                authorFilter.SelectedIndex = 0;

                allBooks = context.Books.Include(x => x.Genre).Include(x => x.Author).ToList();
                bookList.ItemsSource = allBooks;

                // Remove previous event handlers to avoid duplicates
                genreFilter.SelectionChanged -= FilterBooks;
                authorFilter.SelectionChanged -= FilterBooks;

                // Add event handlers
                genreFilter.SelectionChanged += FilterBooks;
                authorFilter.SelectionChanged += FilterBooks;

                // Ensure the ComboBoxes have a selected item
                if (genreFilter.SelectedIndex == -1) genreFilter.SelectedIndex = 0;
                if (authorFilter.SelectedIndex == -1) authorFilter.SelectedIndex = 0;


            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterBooks(sender, e);
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

        private void Ref_Click(object sender, RoutedEventArgs e)
        {
            // Clear existing data
            allBooks.Clear();
            bookList.ItemsSource = null;
            genreFilter.ItemsSource = null;
            authorFilter.ItemsSource = null;
            searchBook.Text = string.Empty;

            context = new LikeBerryContext();

            LoadBookData();

            this.UpdateLayout();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (bookList.SelectedItem is Book selectedBook)
            {
                var book = context.Books.FirstOrDefault(x => x.BookId == selectedBook.BookId);
                if (book != null)
                {
                    EditBook editBook = new EditBook(book, currentUser);
                    editBook.Show();
                }
            }
            else
            {
                MessageBox.Show("Please select a book to edit.");
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Perform any necessary cleanup
            context.Dispose();

            // Create and show the login window
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();

            // Close the current window
            this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddBook addBook = new AddBook(currentUser);
            addBook.Show();
        }

        public bool ConfirmationMessage(string message)
        {
            MessageBoxResult result = MessageBox.Show(message, "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            return result == MessageBoxResult.OK;
        }

        private void Delete_Book(object sender, RoutedEventArgs e)
        {
            bool check = false;
            if (bookList.SelectedItem is Book selectedBook)
            {
                var book = context.Books.FirstOrDefault(x => x.BookId == selectedBook.BookId);
                if (book != null)
                {
                    string message = "Are you sure you want to delete the book?";
                    check = ConfirmationMessage(message);

                    if (check == false)
                    {
                        return;
                    }
                    context.Remove(book);
                    context.SaveChanges();
                    LoadBookData();
                    MessageBox.Show("Deleted book successfully", "Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a book to delete.");
            }
        }

        public void LoadUserData()
        {
            try
            {
                var roles = context.Roles.ToList();
                roles.Insert(0, new Role { RoleId = 0, RoleName = "All Roles" });
                roleFilter.ItemsSource = roles;
                roleFilter.SelectedIndex = 0;

                allUsers = context.Users.Include(x => x.Role).ToList();
                userList.ItemsSource = allUsers;

                // Remove previous event handlers to avoid duplicates
                roleFilter.SelectionChanged -= FilterUsers;

                // Add event handlers
                roleFilter.SelectionChanged += FilterUsers;

                // Ensure the ComboBox has a selected item
                if (roleFilter.SelectedIndex == -1) roleFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading user data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchUserTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterUsers(sender, e);
        }

        private void FilterUsers(object sender, EventArgs e)
        {
            string searchText = searchUser.Text?.ToLower() ?? string.Empty;

            int selectedRoleId = 0;

            if (roleFilter.SelectedItem is Role selectedRole)
            {
                selectedRoleId = selectedRole.RoleId;
            }

            var filteredUsers = allUsers?.Where(user =>
                ((user.FullName?.ToLower().Contains(searchText) ?? false) ||
                (user.Email?.ToLower().Contains(searchText) ?? false)) &&
                (selectedRoleId == 0 || user.RoleId == selectedRoleId)
            ).ToList();

            userList.ItemsSource = filteredUsers;
        }

        private void RefreshUsers_Click(object sender, RoutedEventArgs e)
        {
            allUsers.Clear();
            userList.ItemsSource = null;
            roleFilter.ItemsSource = null;
            searchUser.Text = string.Empty;

            context = new LikeBerryContext();

            // Reload all data
            LoadUserData();

            // Force the UI to update
            this.UpdateLayout();
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUserPage addUserPage = new AddUserPage(currentUser);
            addUserPage.Show();
        }

        private void EditCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (userList.SelectedItem is User user)
            {
                var findUser = context.Users.FirstOrDefault(x => x.UserId == user.UserId);
                if (findUser != null)
                {
                    EditUser editUser = new EditUser(findUser, currentUser);
                    editUser.Show();
                }
            }
            else
            {
                MessageBox.Show("Please select an user to edit.");
            }
        }

        public void LoadBookingData()
        {
            try
            {
                allBookings = context.Bookings.Include(x => x.User).Where(x => x.IsFinished == false)
                    .Include(x => x.BookingDetails)
                    .OrderByDescending(x => x.BookingDate).ToList();
                bookingList.ItemsSource = allBookings;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading booking data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchBookingTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterBookings();
        }

        private void FilterBookings(object sender = null, SelectionChangedEventArgs e = null)
        {
            string searchText = searchBooking.Text?.Trim() ?? string.Empty;

            var filteredBookings = allBookings.Where(booking =>
                (string.IsNullOrEmpty(searchText) || booking.User.UserId.ToString().Contains(searchText))
            ).ToList();

            bookingList.ItemsSource = filteredBookings;
        }

        private void RefreshBookings_Click(object sender, RoutedEventArgs e)
        {
            // Clear existing data
            allBookings.Clear();
            bookingList.ItemsSource = null;
            searchBooking.Text = string.Empty;

            context = new LikeBerryContext();

            LoadBookingData();

            this.UpdateLayout();
        }

        private void ApproveBooking_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Booking selectedBooking)
            {
                try
                {
                    var choice = MessageBox.Show("Are you sure want to approve this submission?",
                        "Confirmation", MessageBoxButton.OKCancel,
                    MessageBoxImage.Question);

                    if (choice == MessageBoxResult.Cancel)
                    {
                        return;
                    }

                    // Update booking status
                    selectedBooking.Status = "Approved";
                    selectedBooking.ProcessedBy = currentUser.UserId;

                    // Get the associated booking details
                    var bookingDetails = context.BookingDetails
                        .Where(x => x.BookingId == selectedBooking.BookingId)
                        .ToList();

                    foreach (var detail in bookingDetails)
                    {
                        // Get the associated book
                        var book = context.Books.FirstOrDefault(b => b.BookId == detail.BookId);

                        if (book != null)
                        {
                            // Ensure the book has enough quantity
                            if (book.InstockQuantity.HasValue && book.InstockQuantity >= detail.Quantity)
                            {
                                // Decrease the book quantity
                                book.InstockQuantity -= detail.Quantity;
                                selectedBooking.IsFinished = true;
                            }
                            else
                            {
                                throw new InvalidOperationException($"Not enough quantity for book: {book.BookName}");
                            }
                        }
                        else
                        {
                            throw new InvalidOperationException($"Book not found for BookId: {detail.BookId}");
                        }
                    }

                    // Save changes
                    context.SaveChanges();

                    // Refresh the list view
                    FilterBookings(); // Refresh the list view

                    bookingList.ItemsSource = null;
                    bookingList.ItemsSource = context.Bookings
                    .Include(x => x.User)
                    .Include(x => x.ProcessedByNavigation) 
                    .Where(x => x.IsFinished == false)
                    .Include(x => x.BookingDetails)
                    .OrderByDescending(x => x.BookingDate)
                    .ToList();


                    MessageBox.Show("Booking approved successfully and book quantities updated.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error approving booking: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a booking to approve.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



        private void DenyBooking_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Booking selectedBooking)
            {
                var choice = MessageBox.Show("Are you sure want to deny the submission?",
                    "Confirmation", MessageBoxButton.OKCancel,
                    MessageBoxImage.Question);

                if (choice == MessageBoxResult.Cancel)
                {
                    return;
                }
                selectedBooking.Status = "Denied";
                selectedBooking.IsFinished = true;
                selectedBooking.ProcessedBy = currentUser.UserId;

                context.SaveChanges();
                // Refresh the list view
                FilterBookings(); // Refresh the list view

                bookingList.ItemsSource = null;
                bookingList.ItemsSource = context.Bookings
                    .Include(x => x.User)
                    .Include(x => x.ProcessedByNavigation)  // Include ProcessedByNavigation
                    .Where(x => x.IsFinished == false)
                    .Include(x => x.BookingDetails)
                    .OrderByDescending(x => x.BookingDate)
                    .ToList();
                MessageBox.Show("Booking denied successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please select a booking to deny.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AddAuthor_Click(object sender, RoutedEventArgs e)
        {
            AddAuthorPage addAuthorPage = new AddAuthorPage(currentUser);
            addAuthorPage.Show();
        }
    }
}