namespace Keepr.Services;

public class KeepsService
{
  private readonly KeepsRepository _repo;

  public KeepsService(KeepsRepository repo)
  {
    _repo = repo;
  }



  public Keep Create(Keep keepData)
  {
    return _repo.Create(keepData);
  }

  internal List<Keep> Get()
  {
    List<Keep> keeps = _repo.Get();
    return keeps;
  }

  internal Keep GetKeepById(int id, string userId)
  {
    Keep keep = _repo.GetKeepById(id);
    if (keep == null)
    {
      throw new Exception($"no keep at id:{id}");
    }
    if (keep.CreatorId != userId)
    {
      keep.Views++;
      Update(keep);
    }
    return keep;
  }

  internal Keep Update(Keep updateData)
  {
    Keep original = GetKeepById(updateData.Id, updateData.CreatorId);
    if (original.CreatorId != updateData.CreatorId) throw new Exception("Nacho keep");
    original.Name = updateData.Name ?? original.Name;
    original.Description = updateData.Description ?? original.Description;
    original.Img = updateData.Img ?? original.Img;

    _repo.Update(original);
    return original;

  }

  internal string Remove(int id, string userId)
  {
    Keep original = GetKeepById(id, userId);
    if (original.CreatorId != userId)
    {
      throw new Exception("not your keep");
    }
    _repo.Remove(id);
    return $"{original.Name} was removed";
  }

  internal List<Keep> getKeepsByProfileId(string profileId)
  {
    return _repo.getKeepsByProfileId(profileId);
  }
}
