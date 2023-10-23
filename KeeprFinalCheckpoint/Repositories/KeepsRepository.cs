using KeeprFinalCheckpoint.Interfaces;

namespace KeeprFinalCheckpoint.Repositories;
public class KeepsRepository : IRepository<Keep, int>
{
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }


    public Keep Create(Keep keepData)
    {
        string sql = @"
        INSERT INTO keeps
        (creatorId, name, description, img, views, kept)
        VALUES
        (@creatorId, @name, @description, @img, @views, @kept);

        SELECT
            keeps.*,
            accounts.*
        FROM keeps
        JOIN accounts ON accounts.id = keeps.creatorId
        WHERE keeps.id = LAST_INSERT_ID()
        ;";
        Keep newKeep = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
        {
            keep.Creator = account;
            return keep;
        }, keepData).FirstOrDefault();
        return newKeep;
    }
    public List<Keep> Get()
    {
        string sql = @"
        SELECT
        kps.*,
        acc.*
        FROM keeps kps
        JOIN accounts acc ON acc.id = kps.creatorId
        ;";
        List<Keep> keeps = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
        {
            keep.Creator = account;
            return keep;
        }).ToList();
        return keeps;
    }

    public Keep GetById(int keepId)
    {
        string sql = @"
        SELECT
        accounts.*,
        keeps.*
        FROM keeps
        JOIN accounts on accounts.id = keeps.creatorId
        WHERE keeps.id = @keepId
        ;";
        Keep foundKeep = _db.Query<Account, Keep, Keep>(sql, (account, keep) =>
        {
            keep.Creator = account;
            return keep;
        }, new { keepId }).FirstOrDefault();
        return foundKeep;
    }

    // FIXME: work on this more, need to do some front end first. This is where I left off in the back end, come back to this and read below

    // NOTE: need to merge the keeps where vaultKeep.keepId = keep.Id, and where vaultId = vaultKeep.vaultId

    // TODO: we have to merge and use vaultKeep to link the keeps to a specific vault. So, we need to say something to the affect of.... join vaults and keeps, where keep.id = vaultKeep.keepId and join where vault.Id = vaultKeep.vaultId
    // internal List<Keep> GetKeepsInVault(int vaultId)
    // {
    //     //     string sql = @"
    //     //     SELECT
    //     //     keeps.*,
    //     //     accounts.*,
    //     //     vaults.*,
    //     //     vaultKeeps.*
    //     //     FROM keeps
    //     //     JOIN accounts ON accounts.id = vaultKeeps.creatorId
    //     //     JOIN vaults ON vaults.id = vaultKeeps.vaultId
    //     //     JOIN keeps ON keeps.id = vaultsKeeps.keepId
    //     //     WHERE vaultKeeps.vaultId = @vaultId
    //     //     ;";

    //     // List<Keep> foundKeeps = _db.Query<Keep, Account, Vault, VaultKeep, Keep>(sql, (keep, account, vault, vaultKeep)=>
    //     // {
    //     //     keep.Creator = account.Id;
    //     //     return keep;
    //     // })
    // }

    public int Delete(int keepId)
    {
        string sql = @"
        DELETE FROM keeps
        WHERE id = @keepId
            LIMIT 1
        ;";
        int rows = _db.Execute(sql, new { keepId });
        return rows;
    }

    public void Update(Keep updateData)
    {
        string sql = @"
        UPDATE keeps
        SET
        name = @name,
        description = @description,
        img = @img,
        views = @views
        WHERE id = @id
        ;";
        _db.Execute(sql, updateData);
    }

    internal List<Keep> GetKeepsByProfile(string profileId)
    {
        string sql = @"
        SELECT
        keeps.*,
        profile.*
        FROM keeps
            JOIN accounts profile ON profile.id = keeps.creatorId
        WHERE keeps.creatorId = @profileId
        ;";
        List<Keep> foundKeeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }, new { profileId }).ToList();
        return foundKeeps;
    }

    // NOTE ok so I want to say join the keeps and they vaults where vaultKeep.creatorId = userId, vaultKeep.vaultId = vaultId. Then, once we find all the vaultKeeps from the user, where the vaultKeep.vaultId = vaultId, join all of the keeps from the corresponding vaultKeeps where vaultKeep.keepId = keeps.id

    // NOTE: so EG: reports in a restaurant is the same as keeps in a vault

    internal List<Keep> GetKeepsInVault(int vaultId, string userId)
    {
        string sql = @"
        SELECT
            keeps.*,
            keepCreator.*,
            vaults.*,
            vaultCreator.*,
        FROM keeps
            JOIN vaultKeeps keepCreator ON keepCreator.id = keeps.creatorId
            JOIN vaults ON vaults.creatorId = vaultCreator.id
            JOIN vaultKeeps vaultCreator ON vaultCreator.id = vaults.creatorId
        WHERE vaultKeeps.vaultId = 15
        ;";
        List<Keep> keepsInVault = _db.Query<Keep, Vault, Profile, VaultKeep, Keep>(sql, (keep, vault, profile, vaultKeep) =>
        {
            keep.Creator = profile;
            // TODO: need more in this dapper statement
            return keep;
        }, new { vaultId }).ToList();
        return keepsInVault;
    }
}