

namespace KeeprFinalCheckpoint.Services;
public class KeepsService
{
    private readonly KeepsRepository _repo;

    public KeepsService(KeepsRepository repo)
    {
        _repo = repo;
    }

    internal Keep Create(Keep keepData)
    {
        Keep newKeep = _repo.Create(keepData);
        return newKeep;
    }

    // TODO: finish get method after you do the post
    internal List<Keep> Get()
    {
        List<Keep> keeps = _repo.Get();
        return keeps;
    }

    internal Keep GetById(int keepId, string userId, bool increaseViews = false)
    {
        Keep foundKeep = _repo.GetById(keepId);
        if (foundKeep == null) throw new Exception("No keep found.");
        if (increaseViews && foundKeep.CreatorId != userId)
        {
            this.IncreaseViews(foundKeep);
        }
        return foundKeep;
    }

    private void IncreaseViews(Keep foundKeep)
    {
        foundKeep.Views++;
        _repo.Update(foundKeep);
    }
}