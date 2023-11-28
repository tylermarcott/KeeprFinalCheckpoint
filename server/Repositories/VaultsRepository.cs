using KeeprFinalCheckpoint.Interfaces;

namespace KeeprFinalCheckpoint.Repositories;

public class VaultsRepository : IRepository<Vault, int>
{
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
        _db = db;
    }

    public Vault Create(Vault newData)
    {
        string sql = @"
        INSERT INTO vaults
        (creatorId, name, description, img, isPrivate)
        VALUES
        (@creatorId, @name, @description, @img, @isPrivate);

        SELECT
            vaults.*,
            accounts.*
        FROM vaults
        JOIN accounts ON accounts.id = vaults.creatorId
        WHERE vaults.id = LAST_INSERT_ID()
        ;";
        Vault newVault = _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
        {
            vault.Creator = account;
            return vault;
        }, newData).FirstOrDefault();
        return newVault;
    }

    public List<Vault> Get()
    {
        throw new NotImplementedException();
    }

    public Vault GetById(int vaultId)
    {
        string sql = @"
        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON accounts.id = vaults.creatorId
        WHERE vaults.id = @vaultId
        ;";

        Vault foundVault = _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
        {
            vault.Creator = account;
            return vault;
        }, new { vaultId }).FirstOrDefault();
        return foundVault;
    }

    public void Update(Vault updateData)
    {
        string sql = @"
        UPDATE vaults
        SET
        name = @name,
        description = @description,
        img = @img
        WHERE id = @id
        ;";
        _db.Execute(sql, updateData);
    }

    public int Delete(int vaultId)
    {
        string sql = @"
        DELETE FROM vaults
        WHERE id = @vaultId
            LIMIT 1
        ;";
        int rows = _db.Execute(sql, new { vaultId });
        return rows;
    }

    internal List<Vault> GetVaultsByProfile(string accountId)
    {
        string sql = @"
        SELECT
        vaults.*,
        profile.*
        FROM vaults
        JOIN accounts profile ON profile.id = vaults.creatorId
        WHERE vaults.creatorId = @accountId
        ;";

        List<Vault> foundVaults = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        {
            vault.Creator = profile;
            return vault;
        }, new { accountId }).ToList();
        return foundVaults;
    }
}
