


namespace KeeprFinalCheckpoint.Services;

public class VaultKeepsService
{
    private readonly VaultKeepsRepository _repo;

    public VaultKeepsService(VaultKeepsRepository repo)
    {
        _repo = repo;
    }

    internal VaultKeep Create(VaultKeep vaultKeepData)
    {
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
