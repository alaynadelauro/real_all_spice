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
}