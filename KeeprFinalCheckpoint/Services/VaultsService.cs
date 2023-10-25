




namespace KeeprFinalCheckpoint.Services;

public class VaultsService
{
    private readonly VaultsRepository _repo;
    private readonly VaultKeepsRepository _vaultKeepsRepo;

    public VaultsService(VaultsRepository repo, VaultKeepsRepository vaultKeepsRepo)
    {
        _repo = repo;
        _vaultKeepsRepo = vaultKeepsRepo;
    }

    internal Vault Create(Vault vaultData)
    {
        Vault newVault = _repo.Create(vaultData);
        return newVault;
    }


    // FIXME: SYNTAX for check:
    // foundVaults = foundVaults.FindAll(vault => vault.IsPrivate == false || vault.CreatorId == userId)

    internal Vault GetById(int vaultId, string userId)
    {
        Vault foundVault = _repo.GetById(vaultId);
        if (foundVault == null) throw new Exception("No vault was found.");
        if (foundVault.IsPrivate && foundVault.CreatorId != userId) throw new Exception("Vault is private, access denied");
        return foundVault;
    }

    // NOTE: this needs to be done in the vaultKeeps repo.
    internal List<VaultKeepViewModel> GetKeepsInVault(int vaultId, string userId)
    {
        Vault foundVault = this.GetById(vaultId, userId);
        if (foundVault.IsPrivate) throw new Exception("Vault is private, you do not have access.");
        List<VaultKeepViewModel> keepsInVault = _vaultKeepsRepo.GetKeepsInVault(vaultId, userId);
        return keepsInVault;
    }

    internal List<Vault> GetVaultsByAccount(string accountId)
    {
        List<Vault> myVaults = _repo.GetVaultsByProfile(accountId);
        return myVaults;
    }

    internal Vault Update(Vault updateData, string userId)
    {
        Vault original = _repo.GetById(updateData.Id);

        if (original.CreatorId != userId) throw new Exception("This is not your vault to edit!");

        original.Name = updateData.Name ?? original.Name;
        original.Description = updateData.Description ?? original.Description;
        original.Img = updateData.Img ?? original.Img;

        _repo.Update(original);

        return original;
    }

    // NOTE: added userId to this for param, incase something breaks lol
    internal void Delete(int vaultId, string userId)
    {
        Vault foundVault = this.GetById(vaultId, userId);
        if (foundVault == null) throw new Exception("No vault was found.");
        if (foundVault.CreatorId != userId) throw new Exception("This is not your vault to delete");
        int rows = _repo.Delete(vaultId);
        if (rows < 1) throw new Exception("Something unexpected has happened, returned with < 1 rows deleted.");
        if (rows > 1) throw new Exception("Something unexpected has happened, returned with > 1 rows deleted.");
    }
}
