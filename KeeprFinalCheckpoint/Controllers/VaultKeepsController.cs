
namespace KeeprFinalCheckpoint.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
    private readonly VaultKeepsService _vaultKeepsService;
    private readonly Auth0Provider _auth;

    public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth)
    {
        _vaultKeepsService = vaultKeepsService;
        _auth = auth;
    }


    [Authorize]
    [HttpPost]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vaultKeepData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            vaultKeepData.CreatorId = userInfo.Id;
            VaultKeep newVaultKeep = _vaultKeepsService.Create(vaultKeepData);
            return Ok(newVaultKeep);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpDelete("{vaultKeepId}")]
    public async Task<ActionResult<String>> Delete(int vaultKeepId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            _vaultKeepsService.Delete(vaultKeepId, userInfo.Id);
            string message = "VaultKeep successfully deleted";
            return message;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
