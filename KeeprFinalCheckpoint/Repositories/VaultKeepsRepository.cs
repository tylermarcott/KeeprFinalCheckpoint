
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
        ;";
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
