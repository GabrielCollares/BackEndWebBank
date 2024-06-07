public class UpdateCardRequest
{
    public required string NumberCard { get; set; }
    public required string CVC { get; set; }
    public required string Validity { get; set; }
    public required string PasswordCard { get; set; }
}