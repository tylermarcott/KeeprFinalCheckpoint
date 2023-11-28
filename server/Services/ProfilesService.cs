




namespace KeeprFinalCheckpoint.Services;

public class ProfilesService
{
    private readonly AccountService _accountsService;
    private readonly VaultsRepository _vaultsRepo;
    private readonly KeepsRepository _keepsRepo;

    public ProfilesService(AccountService accountsService, KeepsRepository keepsRepo, VaultsRepository vaultsRepo)
    {
        _accountsService = accountsService;
        _keepsRepo = keepsRepo;
        _vaultsRepo = vaultsRepo;
    }
    internal Profile GetById(string profileId)
    {
        Profile foundProfile = _accountsService.GetProfileById(profileId);
        return foundProfile;
    }

    internal List<Keep> GetKeepsByProfile(string profileId)
    {
        // Profile profile = this.GetById(profileId);
        List<Keep> foundKeeps = _keepsRepo.GetKeepsByProfile(profileId);
        return foundKeeps;
    }

    internal List<Vault> GetVaultsByProfile(string profileId)
    {
        List<Vault> foundVaults = _vaultsRepo.GetVaultsByProfile(profileId);
        foundVaults = foundVaults.FindAll(vault => vault.IsPrivate == false);
        return foundVaults;
    }
}
