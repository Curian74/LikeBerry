using LikeBerry.Models;
using Microsoft.IdentityModel.Tokens;
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
    /// Interaction logic for BookingProcessPage.xaml
    /// </summary>
    public partial class BookingProcessPage : Window
    {
        private User currentUser;
        private Book borrowBook;
        LikeBerryContext context = new LikeBerryContext();


        public BookingProcessPage(Book book, User user)
        {
            InitializeComponent();
            this.borrowBook = book;
            this.currentUser = user;
            LoadData();
        }

        public void LoadData()
        {
            // Set the DataContext to the book
            this.DataContext = borrowBook;
            BorrowDurationComboBox.SelectedIndex = 0;
            // Display the book name
            SelectedBookName.Text = borrowBook.BookName;

            // Populate user information if available
            if (currentUser != null)
            {
                FullNameTextBox.Text = currentUser.FullName;
                AddressTextBox.Text = currentUser.Address;
                PhoneNumberTextBox.Text = currentUser.PhoneNumber;
                instock.Text = borrowBook.InstockQuantity.ToString();
            }
        }

        private bool QuantityRegex(string text)
        {
            return Regex.IsMatch(text, "[0-9]");
        }

        private void Booking_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Perform validation checks
                if (string.IsNullOrEmpty(QuantityTextBox.Text)
                    || string.IsNullOrEmpty(FullNameTextBox.Text)
                    || string.IsNullOrEmpty(AddressTextBox.Text)
                    || string.IsNullOrEmpty(PhoneNumberTextBox.Text))
                {
                    MessageBox.Show("Please input all fields", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!QuantityRegex(QuantityTextBox.Text))
                {
                    MessageBox.Show("Please input numeric value for quantity", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (int.Parse(QuantityTextBox.Text) <= 0)
                {
                    MessageBox.Show("Quantity must be greater than zero", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                int quantity = int.Parse(QuantityTextBox.Text);

                // Check if there are enough books in stock
                var findBook = context.Books.FirstOrDefault(x => x.BookId == borrowBook.BookId);
                if (findBook == null || quantity > findBook.InstockQuantity)
                {
                    MessageBox.Show("Cannot borrow more than the current quantity", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var choice = MessageBox.Show("Are you sure you want to submit the booking form?", "Confirmation", MessageBoxButton.OKCancel,
                    MessageBoxImage.Question);

                if (choice == MessageBoxResult.Cancel)
                {
                    return;
                }

                // Extract the numeric part from the selected ComboBox item
                string selectedDuration = (BorrowDurationComboBox.SelectedItem as ComboBoxItem).Content.ToString();
                int dur = int.Parse(new string(selectedDuration.TakeWhile(char.IsDigit).ToArray()));

                // Calculate the return date
                DateTime returnDateTime = DateTime.Now.AddDays(dur);

                // Create a new instance of Booking
                Booking booking = new Booking
                {
                    UserId = currentUser.UserId,
                    BookingDate = DateTime.Now,
                    Status = "Submitted",
                    ReturnDate = DateOnly.FromDateTime(returnDateTime),
                    IsFinished = false
                };

                // Create a new instance of BookingDetail
                BookingDetail bookingDetail = new BookingDetail
                {
                    BookId = borrowBook.BookId,
                    Quantity = quantity,
                    FullName = FullNameTextBox.Text,
                    PhoneNumber = PhoneNumberTextBox.Text,
                    Address = AddressTextBox.Text,
                };

                // Add the BookingDetail to the Booking
                booking.BookingDetails = new List<BookingDetail> { bookingDetail };

                // Add the Booking to the context and save changes
                context.Bookings.Add(booking);
                context.SaveChanges();

                MessageBox.Show("Booking submitted successfully!", "Successful",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                OpenMainWindow();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void OpenMainWindow()
        {
            CustomerHomePage customerHomePage = new CustomerHomePage(currentUser);
            customerHomePage.Show();
            this.Close();
        }

        private void CancelBooking_Click(object sender, RoutedEventArgs e)
        {
            OpenMainWindow();
        }
    }
}
