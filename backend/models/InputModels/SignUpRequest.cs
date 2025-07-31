public class SignUpRequest
{
    public string UserName { get; set; } = null!;
    public string FirstName { get; set; }= null!;
    public string LastName { get; set; } = null!;
    public string PhoneNo { get; set; } = null!;
    public string? UserAddress { get; set; }
    public string password{ get; set; } = null!;
    public string? email{ get; set; }
    public int age{ get; set; }
}