namespace Keepr.Models;

public class VaultKeep
{
  public int Id { get; set; }
  public int VaultId { get; set; }
  public int KeepId { get; set; }

  public string CreatorId { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}

public class VaultedKeep : Keep
{
  public int KeepId { get; set; }
  public int VaultId { get; set; }
  public int VaultKeepId { get; set; }
  public string VaultKeepCreatorId { get; set; }
}