using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class IssueRecord
{
    [Key]
    public int RecordNumber { get; set; }

    // Foreign Key to User
    public string? UserName { get; set; }
    [ForeignKey("UserName")]
    public User? User { get; set; }

    // Foreign Key to Book
    public int BookId { get; set; }
    [ForeignKey("BookId")]
    public Book? Book { get; set; }

    public DateTime IssueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public bool IsReturned { get; set; } = false;
}
