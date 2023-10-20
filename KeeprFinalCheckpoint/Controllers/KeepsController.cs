namespace KeeprFinalCheckpoint.Controllers;
[ApiController]
[Route("api/[controller]")]

public class KeepsController : ControllerBase
{
    private readonly KeepsService _keepsService;

    public KeepsController(KeepsService keepsService)
    {
        _keepsService = keepsService;
    }

    public ActionResult<List<Keep>> Get()
    {
        try
        {

        }
        catch (Exception e)
        {
            BadRequest(e.Message);
        }
    }
}