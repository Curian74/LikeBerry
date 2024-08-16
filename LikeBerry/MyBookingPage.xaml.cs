using LikeBerry.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LikeBerry
{
    public partial class MyBookingPage : Window
    {
        LikeBerryContext context = new LikeBerryContext();
        private User? currentUser;
        private List<Booking> allBookings;

        public MyBookingPage(User user)
        {
            InitializeComponent();
            currentUser = user;
            LoadData();
            statusFilter.SelectedIndex = 0; 
        }

        public void LoadData()
        {
            allBookings = context.Bookings
                .Include(x => x.BookingDetails)
                .ThenInclude(x => x.Book)
                .Where(x => x.UserId == currentUser.UserId)
                .OrderByDescending(x => x.BookingDate)
                .ToList();
            myBookingList.ItemsSource = allBookings;

          
        }

        private void StatusFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            string selectedStatus = ((ComboBoxItem)statusFilter.SelectedItem).Content.ToString();

            if (selectedStatus == "All Statuses")
            {
                myBookingList.ItemsSource = allBookings;
            }
            else
            {
                myBookingList.ItemsSource = allBookings.Where(b => b.Status == selectedStatus).ToList();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (myBookingList.SelectedItem is Booking cancelBook)
                {
                    var choice = MessageBox.Show("Are you sure want to cancel this submission?",
                            "Confirmation", MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (choice == MessageBoxResult.No)
                    {
                        return;
                    }

                    context.Remove(cancelBook);
                    context.SaveChanges();
                    LoadData();
                    MessageBox.Show("Booking canceled successfully",
                        "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }

            catch(Exception ex)
            {
                MessageBox.Show("Unexpected error! Booking canceled fail",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}