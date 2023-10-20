


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

    internal Vault GetById(int vaultId)
    {
        Vault foundVault = _repo.GetById(vaultId);
        if (foundVault == null) throw new Exception("No vault was found.");
        return foundVault;
    }
}
