
namespace KeeprFinalCheckpoint.Controllers;

[ApiController]
[Route("api/vaults")]
public class VaultsController : ControllerBase
{
    private readonly VaultsService _vaultsService;
    private readonly Auth0Provider _auth;

    public VaultsController(VaultsService vaultsService, Auth0Provider auth)
    {
        _vaultsService = vaultsService;
        _auth = auth;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault vaultData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            vaultData.Creator = userInfo;
            Vault newVault = _vaultsService.Create(vaultData);
            return newVault;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}

