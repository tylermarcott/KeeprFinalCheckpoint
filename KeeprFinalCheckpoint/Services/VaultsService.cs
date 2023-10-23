




namespace KeeprFinalCheckpoint.Services;

public class VaultsService
{
    private readonly VaultsRepository _vaultsRepo;
    private readonly KeepsRepository _keepsRepo;

    private readonly VaultKeepsRepository _vaultKeepsRepo;

    public VaultsService(VaultsRepository repo, KeepsRepository keepsRepo, VaultKeepsRepository vaultKeepsRepo)
    {
        _vaultsRepo = repo;
        _keepsRepo = keepsRepo;
        _vaultKeepsRepo = vaultKeepsRepo;
    }

    internal Vault Create(Vault vaultData)
    {
        Vault newVault = _vaultsRepo.Create(vaultData);
        return newVault;
    }

    internal Vault GetById(int vaultId, string userId)
    {
        Vault foundVault = _vaultsRepo.GetById(vaultId);
        if (foundVault == null) throw new Exception("No vault was found.");
        return foundVault;
    }

    // NOTE: this needs to be done in the vaultKeeps repo.
    internal List<VaultKeepViewModel> GetKeepsInVault(int vaultId, string userId)
    {
        List<VaultKeepViewModel> keepsInVault = _vaultKeepsRepo.GetKeepsInVault(vaultId, userId);
        return keepsInVault;
    }

    // internal Task<List<Vault>> GetVaultsByAccount(string userId)
    // {
    //     List<Vault> myVaults = _vaultsRepo.GetVaultsByProfile(userId);
    //     return myVaults;
    // }

    internal Vault Update(Vault updateData)
    {
        Vault original = _vaultsRepo.GetById(updateData.Id);
        original.Name = updateData.Name ?? original.Name;
        original.Description = updateData.Description ?? original.Description;
        original.Img = updateData.Img ?? original.Img;

        _vaultsRepo.Update(original);

        return original;
    }

    // NOTE: added userId to this for param, incase something breaks lol
    internal void Delete(int vaultId, string userId)
    {
        Vault foundVault = this.GetById(vaultId, userId);
        if (foundVault == null) throw new Exception("No vault was found.");
        if (foundVault.CreatorId != userId) throw new Exception("This is not your vault to delete");
        int rows = _vaultsRepo.Delete(vaultId);
        if (rows < 1) throw new Exception("Something unexpected has happened, returned with < 1 rows deleted.");
        if (rows > 1) throw new Exception("Something unexpected has happened, returned with > 1 rows deleted.");
    }

}
