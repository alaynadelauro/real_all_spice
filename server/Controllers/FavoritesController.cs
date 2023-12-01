namespace real_all_spice.Controllers;
[ApiController]
[Route("api/[controller]")]
public class FavoritesController(Auth0Provider auth0Provider, FavoritesService favoritesService) : ControllerBase
{
    private readonly Auth0Provider _auth0Provider = auth0Provider;
    private readonly FavoritesService _favoritesService = favoritesService;
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Favorite>> MakeFavorite([FromBody] Favorite favoriteData)
    {
        try
        {
            Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            favoriteData.AccountId = user.Id;
            Favorite favorite = _favoritesService.MakeFavorite(favoriteData);
            return favorite;
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }


    }
}