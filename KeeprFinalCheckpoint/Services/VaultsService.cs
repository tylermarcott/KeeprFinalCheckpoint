

namespace KeeprFinalCheckpoint.Services;

public class VaultsService
{
    private readonly VaultsRepository _repo;

    public VaultsService(VaultsRepository repo)
    {
        _repo = repo;
    }

    internal Vault Create(Vault vaultData)
    {
        Vault newVault = _repo.Create(vaultData);
        return newVault;
    }
}
