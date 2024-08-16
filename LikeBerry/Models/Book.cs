using System;
using System.Collections.Generic;

namespace LikeBerry.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string BookName { get; set; } = null!;

    public string? Img { get; set; }

    public string Isbn { get; set; } = null!;

    public string? BookTitle { get; set; }

    public DateOnly? IssueDate { get; set; }

    public int? AuthorId { get; set; }

    public int? InstockQuantity { get; set; }

    public int? GenreId { get; set; }

    public bool? Status { get; set; }

    public virtual Author? Author { get; set; }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual Genre? Genre { get; set; }
}
