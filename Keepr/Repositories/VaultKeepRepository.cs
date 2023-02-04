namespace Keepr.Repositories;

public class VaultKeepRepository
{

  private readonly IDbConnection _db;

  public VaultKeepRepository(IDbConnection db)
  {
    _db = db;
  }
  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeep)
  {
    var sql = @"
        INSERT INTO vaultKeeps
        (creatorId, vaultId, keepId)
        VALUES 
        (@CreatorId, @VaultId, @KeepId);
        SELECT LAST_INSERT_ID();
        ";
    int id = _db.ExecuteScalar<int>(sql, vaultKeep);
    vaultKeep.Id = id;
    return vaultKeep;
  }

  internal void DeleteVaultKeep(int vaultKeepId)
  {
    string sql = @"
        DELETE FROM vaultKeeps 
        WHERE id = @vaultKeepId; 
        ";
    _db.Execute(sql, new { vaultKeepId });
  }

  internal VaultKeep GetById(int vaultKeepId)
  {
    string sql = @"
                 SELECT *
                FROM vaultKeeps
                WHERE id = @vaultKeepId
                ;";
    return _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();

  }

  internal List<VaultedKeep> GetKeepsByVaultId(int vaultId)
  {
    string sql = @"
            SELECT
            vKeep.*,
            vKeep.id AS VaultKeepId,
            COUNT(vKeep.id) AS Kept,
            k.*,
            a.*
            FROM vaultKeeps vKeep
            JOIN accounts a ON a.id = vKeep.creatorId
            JOIN keeps k ON k.id =  vKeep.keepId
            WHERE vKeep.vaultId = @vaultId
            GROUP BY vKeep.id
                ;";

    return _db.Query<VaultedKeep, Profile, VaultedKeep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      keep.VaultKeepCreatorId = profile.Id;
      return keep;
    }, new { vaultId }).ToList();
  }
}
