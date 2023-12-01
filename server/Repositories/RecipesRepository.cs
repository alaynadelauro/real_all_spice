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
        SELECT 
        recipes.*,
        accounts.* 
        FROM recipes
        JOIN accounts
        ON recipes.creatorId = accounts.id
        WHERE recipes.id = LAST_INSERT_ID();";
        Recipe recipe = _db.Query<Recipe, Account, Recipe>(sql, RecipeBuilder, recipeData).FirstOrDefault();
        return recipe;
    }
    internal List<Recipe> GetRecipes()
    {
        string sql = @"SELECT recipes.*, accounts.* FROM recipes JOIN accounts ON recipes.creatorId = accounts.id;";
        List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, RecipeBuilder).ToList();
        return recipes;
    }
    internal Recipe GetRecipeById(int recipeId)
    {
        string sql = "SELECT recipes.*, accounts.* FROM recipes JOIN accounts ON recipes.creatorId = accounts.id WHERE recipes.id = @recipeId;";
        Recipe recipe = _db.Query<Recipe, Account, Recipe>(sql, RecipeBuilder, new { recipeId }).FirstOrDefault();
        return recipe;
    }
    internal Recipe UpdateRecipe(Recipe foundRecipe)
    {
        string sql = @"UPDATE recipes SET
        title = @Title,
        instructions = @Instructions,
        img = @Img,
        category = @Category
        WHERE id = @id;";
        _db.Execute(sql, foundRecipe);
        return foundRecipe;
    }
    internal void RemoveRecipe(int recipeId)
    {
        string sql = @"DELETE FROM recipes WHERE id = @recipeId LIMIT 1;";
        _db.Execute(sql, new { recipeId });
    }
    private Recipe RecipeBuilder(Recipe recipe, Account account)
    {
        recipe.Creator = account;
        return recipe;
    }
}