namespace real_all_spice.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController(AccountService accountService, Auth0Provider auth0Provider, FavoritesService favoritesService) : ControllerBase
{
  private readonly AccountService _accountService = accountService;
  private readonly Auth0Provider _auth0Provider = auth0Provider;
  private readonly FavoritesService _favoritesService = favoritesService;

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
  [Authorize]
  [HttpPut("{accountId}")]
  public async Task<ActionResult<Account>> EditAccount(string accountId, [FromBody] Account accountData)
  {
    Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
    Account account = _accountService.EditAccount(user, accountId, accountData);
    return account;
  }
  [Authorize]
  [HttpGet("favorites")]
  public async Task<ActionResult<List<RecipeFavorite>>> GetAccountFavorites()
  {
    try
    {
      Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string userId = user.Id;
      List<RecipeFavorite> favorites = _favoritesService.GetAccountFavorites(userId);
      return Ok(favorites);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }
}
