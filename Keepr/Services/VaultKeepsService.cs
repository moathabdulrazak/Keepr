namespace Keepr.Services;

public class VaultKeepsService
{

  private readonly VaultsRepository _vaultRepo;
  private readonly KeepsRepository _keepRepo;
  private readonly VaultsService _vaultService;
  private readonly VaultKeepRepository _vaultKeepRepo;

  public VaultKeepsService(VaultsRepository vaultRepo, KeepsRepository keepRepo, VaultsService vaultService, VaultKeepRepository vaultKeepRepo)
  {
    _vaultRepo = vaultRepo;
    _keepRepo = keepRepo;
    _vaultService = vaultService;
    _vaultKeepRepo = vaultKeepRepo;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeep)
  {
    Vault vault = _vaultRepo.GetVaultById(vaultKeep.VaultId);
    Keep keep = _keepRepo.GetKeepById(vaultKeep.KeepId);
    keep.Kept++;
    if (vault == null)
    {
      throw new Exception("Invalid");
    }
    if (vault.CreatorId != vaultKeep.CreatorId)
    {
      throw new Exception("not your vault.");
    }
    VaultKeep newVaultKeep = _vaultKeepRepo.CreateVaultKeep(vaultKeep);

    return newVaultKeep;
  }

  public VaultKeep GetById(int id)
  {
    VaultKeep vaultKeep = _vaultKeepRepo.GetById(id);
    return vaultKeep;
  }
  internal void DeleteVaultKeep(int vaultKeepId, string accountId)
  {
    VaultKeep vaultKeep = GetById(vaultKeepId);
    Keep keep = _keepRepo.GetKeepById(vaultKeep.KeepId);
    keep.Kept--;
    if (vaultKeep == null)
    {
      throw new Exception("Invalid vaultKeep Id");
    }
    if (vaultKeep.CreatorId != accountId)
    {
      throw new Exception("Unauthorized not yours...");
    }
    _vaultKeepRepo.DeleteVaultKeep(vaultKeepId);
  }

  internal List<VaultedKeep> GetVaultedKeepById(int vaultId, string userId)
  {
    Vault vault = _vaultService.GetVaultById(vaultId, userId);
    return _vaultKeepRepo.GetKeepsByVaultId(vaultId);
  }

  internal List<Vault> getVaultsByProfileId(string profileId)
  {

    return _vaultRepo.getVaultsByProfileId(profileId);
  }
}