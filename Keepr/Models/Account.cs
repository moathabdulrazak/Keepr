namespace Keepr.Models;

public class Account : Profile
{
  public string Email { get; set; }
  public new string Id { get; set; }
  public new string Name { get; set; }
  public new string Picture { get; set; }
}
