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
        (@creatorId, @name, @description, @img, @views, @kept)

        SELECT
        kps.*,
        acc.*
        FROM keeps kps
        JOIN accounts acc ON acc.id = kps.creatorId
        WHERE kps.id = LAST_INSERT_ID()
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

    public int Delete(int id)
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