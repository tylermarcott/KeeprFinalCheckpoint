

namespace KeeprFinalCheckpoint.Services;
public class KeepsService
{
    private readonly KeepsRepository _repo;

    private readonly VaultsService _vaultsService;

    public KeepsService(KeepsRepository repo, VaultsService vaultsService)
    {
        _repo = repo;
        _vaultsService = vaultsService;
    }

    internal Keep Create(Keep keepData)
    {
        Keep newKeep = _repo.Create(keepData);
        return newKeep;
    }

    internal List<Keep> Get()
    {
        List<Keep> keeps = _repo.Get();
        return keeps;
    }

    internal Keep GetById(int keepId)
    {
        Keep foundKeep = _repo.GetById(keepId);
        if (foundKeep == null) throw new Exception("No keep found.");

        foundKeep.Views++;
        this.Update(foundKeep);

        return foundKeep;
    }



    internal Keep Update(Keep updateData, int keepId, string userId)
    {
        Keep original = _repo.GetById(keepId);
        if (original.CreatorId != userId) throw new Exception("Unauthorized to edit!");
        original.Name = updateData.Name ?? original.Name;
        original.Description = updateData.Description ?? original.Description;
        original.Img = updateData.Img ?? original.Img;
        original.Kept = updateData.Kept;

        _repo.Update(original);

        return original;
    }

    // NOTE: this update fxn is just specifically for updating the views
    internal void Update(Keep updateData)
    {
        _repo.Update(updateData);
    }

    internal void Delete(int keepId, string userId)
    {
        Keep foundKeep = _repo.GetById(keepId);
        if (foundKeep == null) throw new Exception("No keep found.");
        if (foundKeep.CreatorId != userId) throw new Exception("This is not your keep to delete.");
        int rows = _repo.Delete(keepId);
        if (rows < 1) throw new Exception("Something unexpected has happened, returned with < 1 rows deleted.");
        if (rows > 1) throw new Exception("Something unexpected has happened, returned with > 1 rows deleted.");
    }

    private void IncreaseViews(Keep foundKeep)
    {
        foundKeep.Views++;
        _repo.Update(foundKeep);
    }

}