namespace KeeprFinalCheckpoint.Models;

public class Keep
{
    public int Id { get; set; }
    public string CreatorId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Img { get; set; }
    public int Views { get; set; }
    public int Kept { get; set; }
    public Profile Creator { get; set; }
}

// NOTE: this is the key to attaching the vaultKeepId to the keep when we do our dapper statement. Without this, the dapper statement is impossible to create for getting keeps in a vault.

public class VaultKeepViewModel : Keep
{
    public int VaultKeepId { get; set; }
    public string ProfileId { get; set; }
}
