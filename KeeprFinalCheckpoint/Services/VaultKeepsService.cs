
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
        return vaultKeepData;
    }
}
