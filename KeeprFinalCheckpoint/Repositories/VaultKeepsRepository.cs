
using KeeprFinalCheckpoint.Interfaces;

namespace KeeprFinalCheckpoint.Repositories;

public class VaultKeepsRepository : IRepository<VaultKeep, int>
{
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    public VaultKeep Create(VaultKeep newData)
    {
        string sql = @"
        INSERT INTO vaultKeeps
        (vaultId, keepId, creatorId)
        VALUES
        (@vaultId, @keepId, @creatorId);

        SELECT
            vaultKeeps.*,
            accounts.*
        FROM vaultKeeps
        JOIN accounts ON accounts.id = vaultKeeps.creatorId
        WHERE vaultKeeps.id = LAST_INSERT_ID()
        ;";
        int lastInsertId = _db.ExecuteScalar<int>(sql, newData);
        newData.Id = lastInsertId;
        return newData;
    }

    // NOTE: ok, so this is how the data relationship works with VaultKeep: Vaultkeep points at keep, vault, and account. That means we need to Join with creator, keep, and vault I believe? I feel like Mick said that we only needed to do 2 joins, but maybe we have to actually do 3. That would make the most sense to me. I believe a good example of this is cult membership/ cultist in the Instacult example.
    //NOTE; vaultKeeps are just keeps that are put into a vault. It's almost like a copy, where we are taking who is making the vaultKeep (user/account), which keep they want to save, in which vault. That is the many-to-many relationship, between vaultkeep and the 3 others.

    // FIXME: work on this more, need to do some front end first. This is where I left off in the back end, come back to this and read below

    // NOTE: need to merge the keeps where vaultKeep.keepId = keep.Id, and where vaultId = vaultKeep.vaultId

    // TODO: we have to merge and use vaultKeep to link the keeps to a specific vault. So, we need to say something to the affect of.... join vaults and keeps, where keep.id = vaultKeep.keepId and join where vault.Id = vaultKeep.vaultId


    // NOTE: just because keeps are the final endpoint does NOT mean that keeps is where the repo needs to be. In this case, vaults/vaultId/keeps is going to be in the vaultKeeps repo.
    internal List<Keep> GetKeepsInVault(int vaultId, string userId)
    {
        string sql = @"
        SELECT
            vaultKeeps.*,
            keeps.*,
            vaults.*,
            profiles.*
        FROM vaultKeeps
            JOIN keeps ON keeps.id = vaultKeeps.keepId
            JOIN vaults ON vaults.id = vaultKeeps.vaultId
            JOIN accounts profiles ON profiles.id = vaultKeeps.creatorId
        WHERE vaultKeeps.vaultId = @vaultId
        ;";

        List<Keep> foundKeeps = _db.Query<VaultKeep, Keep, Vault, Profile, VaultKeep>(sql, (vaultKeep, keep, vault, profile) =>
        {
            return foundKeeps;
        }, new { vaultId }).FirstOrDefault();
        return foundKeeps;
    }


    public int Delete(int id)
    {
        throw new NotImplementedException();
    }

    public List<VaultKeep> Get()
    {
        throw new NotImplementedException();
    }

    public VaultKeep GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(VaultKeep updateData)
    {
        throw new NotImplementedException();
    }
}
