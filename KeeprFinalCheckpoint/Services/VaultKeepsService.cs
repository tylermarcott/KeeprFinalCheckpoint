


namespace KeeprFinalCheckpoint.Services;

public class VaultKeepsService
{
    private readonly VaultKeepsRepository _repo;
    private readonly VaultsService _vaultsService;

    public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService)
    {
        _repo = repo;
        _vaultsService = vaultsService;
    }

    internal VaultKeep Create(VaultKeep vaultKeepData, string userId)
    {
        // FIXME: need to prevent user from being about to create a vaultKeep? I'm kind of confused of what I am trying to prevent, but this is the last test I need to pass.
        Vault foundVault = _vaultsService.GetById(vaultKeepData.VaultId, userId);
        if (foundVault.IsPrivate) throw new Exception("This vault is private, you cannot access this.");
        if (foundVault.CreatorId != userId) throw new Exception("You are not allowed to create this vault keep.");
        VaultKeep newVaultKeep = _repo.Create(vaultKeepData);
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
