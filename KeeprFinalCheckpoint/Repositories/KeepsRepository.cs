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
}