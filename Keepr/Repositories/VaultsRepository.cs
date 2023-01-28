namespace Keepr.Repositories;

public class VaultsRepository
{
  private readonly IDbConnection _db;

  public VaultsRepository(IDbConnection db)
  {
    _db = db;
  }

  public Vault Create(Vault vaultData)
  {
    string sql = @"
      INSERT INTO vaults
        (name, `creatorId`, `img`, description, isPrivate)
      VALUES
        (@Name, @CreatorId, Img, @Description, @IsPrivate);
      SELECT LAST_INSERT_ID()
      ;";

    vaultData.Id = _db.ExecuteScalar<int>(sql, vaultData);
    return vaultData;
  }

  internal Vault GetVaultById(int id)
  {
    string sql = @"
    SELECT
    v.*,
    ac.*
    FROM vaults v
    JOIN accounts ac ON ac.id = v.creatorId
    WHERE v.id = @id;
    ";
    return _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
      {
        vault.Creator = account;
        return vault;
      }, new { id }).FirstOrDefault();
  }

  internal void Remove(int id)
  {
    string sql = @"
    DELETE FROM vaults
    WHERE id = @id;
    ";
    _db.Execute(sql, new { id });

  }

  internal bool Update(Vault update)
  {
    string sql = @"
      UPDATE vaults SET
      name = @name,
      isPrivate = @isPrivate
      WHERE id = @id;
    ";
    int rows = _db.Execute(sql, update);
    return rows > 0;
  }

}

