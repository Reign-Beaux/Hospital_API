namespace Hospital.Application.Models
{
  public class ValidationResult
  {
    public bool IsValid { get; set; } = true;
    public List<string> Errors { get; set; } = new List<string>();
  }
}
