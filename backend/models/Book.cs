using System.ComponentModel.DataAnnotations;

public class Book
{
    [Key]
    public int BookId { get; set; }

    public string BookName { get; set; }
    public string AuthorName { get; set; }
    public int BookCount { get; set; }
    public string BookDescription { get; set; }
    public string ImagePath { get; set; }

    // Navigation property
    public List<IssueRecord> IssueRecords { get; set; }
}