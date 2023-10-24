
namespace KeeprFinalCheckpoint.Controllers;

[ApiController]
[Route("api/vaults")]
public class VaultsController : ControllerBase
{
    private readonly KeepsService _keepsService;
    private readonly VaultsService _vaultsService;
    private readonly Auth0Provider _auth;

    public VaultsController(VaultsService vaultsService, Auth0Provider auth, KeepsService keepsService)
    {
        _vaultsService = vaultsService;
        _auth = auth;
        _keepsService = keepsService;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault vaultData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            vaultData.CreatorId = userInfo.Id;
            vaultData.Creator = userInfo;
            Vault newVault = _vaultsService.Create(vaultData);
            return newVault;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }




    [HttpGet("{vaultId}")]
    public async Task<ActionResult<Vault>> GetById(int vaultId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            // NOTE: fix for compensating for a person that is logged in or not is putting the elvis operator
            Vault foundVault = _vaultsService.GetById(vaultId, userInfo?.Id);
            return foundVault;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{vaultId}/keeps")]
    public async Task<ActionResult<List<VaultKeepViewModel>>> GetKeepsInVault(int vaultId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            List<VaultKeepViewModel> keepsInVault = _vaultsService.GetKeepsInVault(vaultId, userInfo?.Id);
            return keepsInVault;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [Authorize]
    [HttpPut("{vaultId}")]
    public async Task<ActionResult<Vault>> Update([FromBody] Vault updateData, int vaultId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            // updateData.CreatorId = userInfo.Id;
            updateData.Id = vaultId;
            Vault updatedVault = _vaultsService.Update(updateData, userInfo.Id);
            return updateData;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpDelete("{vaultId}")]
    public async Task<ActionResult<string>> Delete(int vaultId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            _vaultsService.Delete(vaultId, userInfo.Id);
            string message = "Vault successfully deleted";
            return message;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


}

