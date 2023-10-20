using KeeprFinalCheckpoint.Interfaces;

namespace KeeprFinalCheckpoint.Repositories;
public class KeepsRepository : IRepository<Keep, int>
{
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }


    public Keep Create(Keep newData)
    {
        throw new NotImplementedException();
    }

    public int Delete(int id)
    {
        throw new NotImplementedException();
    }

    public List<Keep> Get()
    {
        throw new NotImplementedException();
    }

    public Keep GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(Keep updateData)
    {
        throw new NotImplementedException();
    }
}