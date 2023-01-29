namespace Keepr.Services;

public class VaultsService
{
  private readonly VaultsRepository _repo;

  public VaultsService(VaultsRepository repo)
  {
    _repo = repo;
  }
  public Vault Create(Vault vaultData)
  {
    return _repo.Create(vaultData);
  }

  internal Vault GetVaultById(int id, string userId)
  {
    Vault vault = _repo.GetVaultById(id);
    var creator = vault.CreatorId == userId;
    var privateVault = vault.IsPrivate == true;
    if (!creator && privateVault)
    {
      throw new Exception("privated vault");
    }
    return vault;
  }

  internal Vault Update(Vault updateData)
  {
    Vault original = GetVaultById(updateData.Id, updateData.CreatorId);
    if (original.CreatorId != updateData.CreatorId) throw new Exception("Nacho vault");
    original.Name = updateData.Name ?? original.Name;
    original.IsPrivate = updateData.IsPrivate ?? original.IsPrivate;

    _repo.Update(original);
    return original;
  }

  internal string Remove(int id, string userId)
  {
    Vault original = GetVaultById(id, userId);
    if (original.CreatorId != userId)
    {
      throw new Exception("not your keep");
    }
    _repo.Remove(id);
    return $"{original.Name} was removed";
  }
}
