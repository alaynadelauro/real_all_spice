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
    internal Favorite GetFavoriteById(string userId)
    {
        Favorite favorite = _repo.GetFavoriteById(userId) ?? throw new Exception("If this favorite existed, it was gone before you tried to remove it");
        return favorite;
    }
    internal string Unlike(int favoriteId, string userId)
    {
        Favorite favorite = GetFavoriteById(userId);
        if (favorite.AccountId != userId)
        {
            throw new Exception("Your account's id isn't the same as the person who made this their favorite");
        }
        _repo.Unlike(favoriteId);
        return "Favorite successfully removed";
    }
}