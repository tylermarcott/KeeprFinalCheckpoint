


namespace KeeprFinalCheckpoint.Services;

public class VaultKeepsService
{
    private readonly VaultKeepsRepository _repo;
    private readonly VaultsService _vaultsService;

    private readonly KeepsService _keepsService;

    public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService, KeepsService keepsService)
    {
        _repo = repo;
        _vaultsService = vaultsService;
        _keepsService = keepsService;
    }

    // FIXME: whenever a vault keep is created, have to increase keep count and update to corresponding keep.
    // NOTE: so to create a vaultKeep, I need a vault id and a keep id, it seems like I need to be able to create an object that can hold the creatorId, keepId, and vaultId in order to pass this object in and treat it as a sort of automatic form submission.
    // FIXME: need to be able to increment kept count here, but it isn't working.

    internal VaultKeep Create(VaultKeep vaultKeepData, string userId)
    {
        Vault foundVault = _vaultsService.GetById(vaultKeepData.VaultId, userId);
        if (foundVault.IsPrivate) throw new Exception("This vault is private, you cannot access this.");
        if (foundVault.CreatorId != userId) throw new Exception("You are not allowed to create this vault keep.");
        VaultKeep newVaultKeep = _repo.Create(vaultKeepData);

        Keep foundKeep = _keepsService.GetById(newVaultKeep.KeepId);
        foundKeep.Views--;
        foundKeep.Kept++;
        _keepsService.Update(foundKeep);

        return newVaultKeep;
    }

    internal VaultKeep GetById(int vaultKeepId)
    {
        VaultKeep foundVaultKeep = _repo.GetById(vaultKeepId);
        return foundVaultKeep;
    }

    internal void Delete(int vaultKeepId, string userId)
    {
        VaultKeep foundVaultKeep = this.GetById(vaultKeepId);
        if (foundVaultKeep == null) throw new Exception("Invalid vaultKeep id");
        if (userId != foundVaultKeep.CreatorId) throw new Exception("This is not your vaultKeep to delete.");
        int rows = _repo.Delete(vaultKeepId);
        if (rows > 1) throw new Exception("Something went wrong");
        if (rows < 1) throw new Exception("Something went way wrong");
    }
}
