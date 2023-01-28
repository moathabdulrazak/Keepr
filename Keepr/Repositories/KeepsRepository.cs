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
(name, description, Img, creatorId )
VALUES
(@Name, @Description, @Img, @CreatorId);
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
ac.*
FROM keeps k 
JOIN accounts ac on ac.id = k.creatorId;
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
    ac.*
    FROM keeps k
    JOIN accounts ac ON ac.id = k.creatorId
    WHERE k.id = @id;
    ";
    return _db.Query<Keep, Account, Keep>(sql, (Keep, account) =>
      {
        Keep.Creator = account;
        return Keep;
      }, new { id }).FirstOrDefault();
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
