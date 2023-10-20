
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
    internal List<Keep> GetKeeps()
    {
        List<Keep> keeps = _repo.Get();
        return keeps;
    }
}