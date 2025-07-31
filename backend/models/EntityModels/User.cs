using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNo { get; set; }
    public string? UserAddress { get; set; }
    public string? password{ get; set; }
    public string? email{ get; set; }
    public int? age{ get; set; }

    // Navigation property
    public List<IssueRecord>? IssueRecords { get; set; }
}