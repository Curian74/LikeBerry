using System;
using System.Collections.Generic;

namespace LikeBerry.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? UserId { get; set; }

    public DateTime? BookingDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public string? Status { get; set; }

    public bool? IsFinished { get; set; }

    public int? ProcessedBy { get; set; }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual User? ProcessedByNavigation { get; set; }

    public virtual User? User { get; set; }
}
