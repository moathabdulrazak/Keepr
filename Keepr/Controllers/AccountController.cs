namespace Keepr.Controllers;

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
  [HttpGet("{vaults}")]
  [Authorize]
  public async Task<ActionResult<List<Vault>>> GetMyVaults()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Vault> vault = _accountService.GetMyVaults(userInfo.Id);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  // [HttpGet("{keeps}")]
  // [Authorize]
  // public async Task<ActionResult<List<Keep>>> GetMyKeeps()
  // {
  //   try
  //   {
  //     Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
  //     List<Keep> keep = _accountService.GetMyKeeps(userInfo.Id);
  //     return Ok(keep);
  //   }
  //   catch (Exception e)
  //   {
  //     return BadRequest(e.Message);
  //   }
  // }


  [HttpPut]
  [Authorize]

  public async Task<ActionResult<Account>> Edit(string id, [FromBody] Account updateData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      updateData.Id = userInfo.Id;
      updateData.Id = id;
      Account account = _accountService.Edit(updateData, userInfo.Email);
      return Ok(account);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}
