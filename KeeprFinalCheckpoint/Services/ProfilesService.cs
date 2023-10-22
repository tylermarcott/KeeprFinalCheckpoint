

namespace KeeprFinalCheckpoint.Services;

public class ProfilesService
{
    private readonly AccountService _accountsService;

    public ProfilesService(AccountService accountsService)
    {
        _accountsService = accountsService;
    }
    internal Profile GetById(string profileId)
    {
        Profile foundProfile = _accountsService.GetProfileById(profileId);
        return foundProfile;
    }
}
