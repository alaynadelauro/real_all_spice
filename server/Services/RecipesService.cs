namespace real_all_spice.Services;
public class RecipesService(RecipesRepository recipesRepository)
{
    private readonly RecipesRepository _repository = recipesRepository;
    internal Recipe AddRecipe(Recipe recipeData)
    {
        Recipe recipe = _repository.AddRecipe(recipeData);
        return recipe;
    }
}