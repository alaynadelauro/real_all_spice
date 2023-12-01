namespace real_all_spice.Repositories;
public class FavoritesRepository(IDbConnection db)
{
    private readonly IDbConnection _db = db;
    internal Favorite MakeFavorite(Favorite favoriteData)
    {
        string sql = @"INSERT INTO favorites (accountId, recipeId) VALUES (@AccountId, @RecipeId);
        SELECT * FROM favorites WHERE id = LAST_INSERT_ID();";
        Favorite favorite = _db.Query<Favorite>(sql, favoriteData).FirstOrDefault();
        return favorite;
    }
    internal List<RecipeFavorite> GetAccountFavorites(string userId)
    {
        string sql = @"SElECT * FROM favorites 
        JOIN recipes ON favorites.recipeId = recipes.id 
        JOIN accounts ON favorites.accountId = accounts.id
        WHERE favorites.accountId = @userId;";
        List<RecipeFavorite> favorites = _db.Query<Favorite, RecipeFavorite, Account, RecipeFavorite>(sql, (favorite, recipeFavorite, account) =>
        {
            recipeFavorite.AccountId = userId;
            recipeFavorite.FavoriteId = favorite.Id;
            recipeFavorite.Creator = account;
            return recipeFavorite;
        }, new { userId }).ToList();
        return favorites;
    }
}