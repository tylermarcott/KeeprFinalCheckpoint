
namespace KeeprFinalCheckpoint.Controllers
{
    [ApiController]
    [Route("api/profiles")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _profilesService;
        private readonly Auth0Provider _auth;

        public ProfilesController(ProfilesService profilesService, Auth0Provider auth, VaultKeepsService vaultsService)
        {
            _profilesService = profilesService;
            _auth = auth;
        }

        [HttpGet("{profileId}")]
        public async Task<ActionResult<Profile>> GetById(string profileId)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);

                Profile foundProfile = _profilesService.GetById(profileId);
                return foundProfile;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{profileId}/keeps")]
        public ActionResult<List<Keep>> GetKeepsByProfile(string profileId)
        {
            try
            {
                List<Keep> foundKeeps = _profilesService.GetKeepsByProfile(profileId);
                return foundKeeps;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{profileId}/vaults")]
        public ActionResult<List<Vault>> GetVaultsByProfile(string profileId)
        {
            try
            {
                List<Vault> foundVaults = _profilesService.GetVaultsByProfile(profileId);
                return foundVaults;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}