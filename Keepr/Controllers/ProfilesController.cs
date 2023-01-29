namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly ProfilesService _profileService;
  private readonly KeepsService _keepService;
  private readonly VaultKeepsService _vaultService;

  public ProfilesController(AccountService accountService, Auth0Provider auth0Provider, ProfilesService profileService, KeepsService keepService, VaultKeepsService vaultService)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _profileService = profileService;
    _keepService = keepService;
    _vaultService = vaultService;
  }


  [HttpGet("{profileId}")]
  public ActionResult<Profile> GetById(string profileId)
  {
    try
    {
      Profile profile = _profileService.GetById(profileId);
      return Ok(profile);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{profileId}/keeps")]
  public ActionResult<List<Keep>> getKeepsByProfileId(string profileId)
  {
    try
    {
      List<Keep> profileKeeps = _keepService.getKeepsByProfileId(profileId);
      return Ok(profileKeeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpGet("{profileId}/vaults")]
  public ActionResult<List<Vault>> getVaultsByProfileId(string profileId)
  {
    try
    {
      List<Vault> profileVault = _vaultService.getVaultsByProfileId(profileId);
      return Ok(profileVault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}