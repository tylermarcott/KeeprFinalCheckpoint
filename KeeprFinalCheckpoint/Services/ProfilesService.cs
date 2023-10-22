



namespace KeeprFinalCheckpoint.Services;

public class ProfilesService
{
    private readonly AccountService _accountsService;
    private readonly KeepsRepository _repo;

    public ProfilesService(AccountService accountsService, KeepsRepository repo)
    {
        _accountsService = accountsService;
        _repo = repo;
    }
    internal Profile GetById(string profileId)
    {
        Profile foundProfile = _accountsService.GetProfileById(profileId);
        return foundProfile;
    }

    internal List<Keep> GetKeepsByProfile(string profileId)
    {
        // Profile profile = this.GetById(profileId);
        List<Keep> foundKeeps = _repo.GetKeepsByProfile(profileId);
        return foundKeeps;
    }
}
