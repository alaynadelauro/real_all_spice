namespace real_all_spice.Services;
public class FavoritesService(FavoritesRepository favoritesRepository)
{
    private readonly FavoritesRepository _repo = favoritesRepository;
    internal Favorite MakeFavorite(Favorite favoriteData)
    {
        Favorite favorite = _repo.MakeFavorite(favoriteData);
        return favorite;
    }
    internal List<RecipeFavorite> GetAccountFavorites(string userId)
    {
        List<RecipeFavorite> favorites = _repo.GetAccountFavorites(userId);
        return favorites;
    }
}