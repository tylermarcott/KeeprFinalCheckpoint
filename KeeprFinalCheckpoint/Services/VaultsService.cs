




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

    // FIXME: there's something wrong with the logic in here. 

    // NOTE: not passing the following tests:
    // edit vault NOTED ✅
    // get vault by id (public) NOTED ✅
    // get a user's vaults
    // get private vault (invalid-auth) NOTED
    // edit vault (invalid auth) NOTED 
    // create vaultKeep (invalid auth) NOTED
    // get keeps in a private vault (invalid auth) NOTED


    internal Vault GetById(int vaultId, string userId)
    {
        // FIXME: this is preventing 2/7 tests to fail, giving same error as the edit vault when it doesn't work
        // FIXME: add syntax to prevent someone from getting a vault that is marked private
        Vault foundVault = _repo.GetById(vaultId);
        if (foundVault == null) throw new Exception("No vault was found.");
        if (foundVault.IsPrivate) throw new Exception("Vault is private, access denied");
        return foundVault;
    }

    // NOTE: this needs to be done in the vaultKeeps repo.
    internal List<VaultKeepViewModel> GetKeepsInVault(int vaultId, string userId)
    {
        // FIXME: need to add syntax to make sure someone can't get keeps in a private vault
        List<VaultKeepViewModel> keepsInVault = _vaultKeepsRepo.GetKeepsInVault(vaultId, userId);
        return keepsInVault;
    }

    internal List<Vault> GetVaultsByAccount(string accountId)
    {
        List<Vault> myVaults = _repo.GetVaultsByProfile(accountId);
        return myVaults;
    }

    internal Vault Update(Vault updateData)
    {
        // FIXME: add auth so that invalid user cannot edit vault. This issue is causing 2/7 tests not to pass
        Vault original = _repo.GetById(updateData.Id);
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
