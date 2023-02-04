namespace Keepr.Services;

public class AccountService
{
  private readonly AccountsRepository _repo;

  public AccountService(AccountsRepository repo)
  {
    _repo = repo;
  }

  internal Account GetProfileByEmail(string email)
  {
    return _repo.GetByEmail(email);
  }

  internal Account GetOrCreateProfile(Account userInfo)
  {
    Account profile = _repo.GetById(userInfo.Id);
    if (profile == null)
    {
      return _repo.Create(userInfo);
    }
    return profile;
  }


  internal List<Vault> GetMyVaults(string creatorId)
  {
    return _repo.GetMyVaults(creatorId);
  }

  internal Account Edit(Account editData, string userEmail)
  {
    Account original = GetProfileByEmail(userEmail);
    original.Name = editData.Name.Length > 0 ? editData.Name : original.Name;
    original.Picture = editData.Picture.Length > 0 ? editData.Picture : original.Picture;
    original.CoverImg = editData.CoverImg.Length > 0 ? editData.CoverImg : original.CoverImg;
    return _repo.Edit(original);
  }

  // internal List<Keep> GetMyKeeps(string creatorId)
  // {
  //   return _repo.GetMyKeeps(creatorId);
  // }
}