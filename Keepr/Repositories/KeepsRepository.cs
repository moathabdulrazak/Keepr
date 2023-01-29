namespace Keepr.Repositories;

public class KeepsRepository
{


  private readonly IDbConnection _db;

  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }


  public Keep Create(Keep keepData)
  {
    string sql = @"
INSERT INTO  keeps
(name, description, Img, creatorId, views )
VALUES
(@Name, @Description, @Img, @CreatorId, @views);
SELECT LAST_INSERT_ID()
";
    keepData.Id = _db.ExecuteScalar<int>(sql, keepData);
    return keepData;
  }

  internal List<Keep> Get()
  {
    string sql = @"
    SELECT
k.*,
COUNT(vk.id) AS Kept,
ac.*
FROM keeps k 
JOIN accounts ac on ac.id = k.creatorId
LEFT JOIN vaultKeeps vk ON vk.keepId = k.id
        GROUP BY k.id
";
    List<Keep> keeps = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
    {
      keep.Creator = account;
      return keep;
    }).ToList();
    return keeps;
  }

  internal Keep GetKeepById(int id)
  {
    string sql = @"
   SELECT 
      k.*,
      COUNT(vKeep.id) AS Kept,
      a.*
      FROM keeps k
      LEFT JOIN vaultKeeps vKeep ON vKeep.keepId = k.Id
      JOIN accounts a ON a.id = k.creatorId
      WHERE k.id = @id
      GROUP BY k.id
      ";
    return _db.Query<Keep, Account, Keep>(sql, (Keep, account) =>
      {
        Keep.Creator = account;
        return Keep;
      }, new { id }).FirstOrDefault();
  }

  internal List<Keep> getKeepsByProfileId(string profileId)
  {
    string sql = @"
    SELECT
    k.*,
    prof.*
    FROM keeps k 
    JOIN accounts prof ON prof.id = k.creatorId
    LEFT JOIN vaultKeeps  vKeep ON vKeep.keepId = k.Id
    WHERE k.creatorId = @profileId;
    ";
    return _db.Query<Keep, Account, Keep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }, new { profileId }).ToList();

  }

  internal void Remove(int id)
  {
    string sql = @"
    DELETE FROM keeps
    WHERE id = @id;
    ";
    _db.Execute(sql, new { id });

  }

  internal bool Update(Keep update)
  {
    string sql = @"
      UPDATE keeps SET
      name = @name,
      description = @description,
      img = @img,
      views = @views
      WHERE id = @id;
    ";
    int rows = _db.Execute(sql, update);
    return rows > 0;
  }


}
