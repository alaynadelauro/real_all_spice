namespace real_all_spice.Repositories;
public class IngredientsRepository(IDbConnection db)
{
    private readonly IDbConnection _db = db;
    internal Ingredient AddIngredient(Ingredient ingredientData)
    {
        string sql = @"INSERT INTO ingredients (name, quantity, recipeId) VALUES(@Name, @Quantity, @RecipeId);
        SELECT * FROM ingredients WHERE id = LAST_INSERT_ID();";
        Ingredient ingredient = _db.Query<Ingredient>(sql, ingredientData).FirstOrDefault();
        return ingredient;
    }
}