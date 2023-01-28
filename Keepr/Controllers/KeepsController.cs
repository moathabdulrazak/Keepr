namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KeepsController : ControllerBase
{
  private readonly KeepsService _keepsService;
  private readonly Auth0Provider _auth;

  public KeepsController(KeepsService keepsService, Auth0Provider auth)
  {
    _keepsService = keepsService;
    _auth = auth;
  }


  [HttpPost]
  [Authorize]

  public async Task<ActionResult<Keep>> Create([FromBody] Keep keepData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      keepData.CreatorId = userInfo.Id;
      Keep keep = _keepsService.Create(keepData);
      keep.Creator = userInfo;
      return Ok(keep);
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

  [HttpGet("{id}")]
  public async Task<ActionResult<Keep>> GetKeepById(int id)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      Keep keep = _keepsService.GetKeepById(id, userInfo?.Id);
      return Ok(keep);
    }
    catch (Exception e)
    {

      return BadRequest(e.Message);
    }
  }

  [HttpPut("{id}")]
  [Authorize]
  public async Task<ActionResult<Keep>> Update(int id, [FromBody] Keep updateData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      updateData.CreatorId = userInfo.Id;
      updateData.Id = id;
      Keep keep = _keepsService.Update(updateData);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ActionResult<string>> Remove(int id)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string message = _keepsService.Remove(id, userInfo.Id);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}
