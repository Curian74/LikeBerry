using System;
using System.Collections.Generic;

namespace LikeBerry.Models;

public partial class BookingDetail
{
    public int BookingDetailId { get; set; }

    public int? BookingId { get; set; }

    public int? BookId { get; set; }

    public int? Quantity { get; set; }

    public string? FullName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Booking? Booking { get; set; }
}
