namespace KeeprFinalCheckpoint.Controllers;
[ApiController]
[Route("api/keeps")]

public class KeepsController : ControllerBase
{
    private readonly KeepsService _keepsService;
    private readonly Auth0Provider _auth;

    public KeepsController(KeepsService keepsService, Auth0Provider auth)
    {
        _keepsService = keepsService;
        _auth = auth;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Keep>> Create([FromBody] Keep keepData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            keepData.CreatorId = userInfo.Id;
            Keep newKeep = _keepsService.Create(keepData);
            return newKeep;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public ActionResult<List<Keep>> Get()
    {
        try
        {
            List<Keep> keeps = _keepsService.Get();
            return Ok(keeps);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{keepId}")]

    public async Task<ActionResult<Keep>> GetById(int keepId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Keep foundKeep = _keepsService.GetById(keepId, userInfo?.Id, true);
            return Ok(foundKeep);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpPut("{keepId}")]

    public async Task<ActionResult<Keep>> Update([FromBody] Keep updateData, int keepId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Keep updatedKeep = _keepsService.Update(updateData, keepId, userInfo.Id);
            return updateData;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpDelete("{keepId}")]

    public async Task<ActionResult<string>> Delete(int keepId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            _keepsService.Delete(keepId, userInfo.Id);
            string message = "Keep successfully deleted.";
            return Ok(message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}