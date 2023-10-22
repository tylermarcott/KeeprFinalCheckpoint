namespace KeeprFinalCheckpoint.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  // TODO: I need to wait to do this until the vaultkeeps are done, because I need to use vaultkeeps in order to get this data I think. Note, ref for this is in AccountController.cs in PostItSharp

  // [Authorize]
  // [HttpGet("vaults")]
  // public async Task<ActionResult<Account>> GetVaultsByAccount()
  // {
  //   try
  //   {
  //     Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
  //     return Ok(_accountService.GetOrCreateProfile(userInfo));
  //   }
  //   catch (Exception e)
  //   {
  //     return BadRequest(e.Message);
  //   }
  // }
}
