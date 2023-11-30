namespace real_all_spice.Repositories;
public class RecipesRepository(IDbConnection db)
{
    private readonly IDbConnection _db = db;
    internal Recipe AddRecipe(Recipe recipeData)
    {
        string sql = @"
        INSERT INTO 
        recipes(title, instructions, img, category, creatorId)
        VALUES(@Title, @Instructions, @Img, @Category, @CreatorId);
        SELECT * FROM recipes WHERE id = LAST_INSERT_ID();";
        Recipe recipe = _db.Query<Recipe>(sql, recipeData).FirstOrDefault();
        return recipe;
    }
    internal List<Recipe> GetRecipes()
    {
        string sql = @"SELECT * FROM recipes;";
        List<Recipe> recipes = _db.Query<Recipe>(sql).ToList();
        return recipes;
    }
}