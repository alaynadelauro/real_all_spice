namespace real_all_spice.Services;
public class RecipesService(RecipesRepository recipesRepository)
{
    private readonly RecipesRepository _repository = recipesRepository;
    internal Recipe AddRecipe(Recipe recipeData)
    {
        Recipe recipe = _repository.AddRecipe(recipeData);
        return recipe;
    }
    internal List<Recipe> GetRecipes()
    {
        List<Recipe> recipes = _repository.GetRecipes();
        return recipes;
    }
    internal Recipe GetRecipeById(int recipeId)
    {
        Recipe recipe = _repository.GetRecipeById(recipeId) ?? throw new Exception($"{recipeId} is not a valid ID");
        ;
        return recipe;
    }
    internal Recipe UpdateRecipe(int recipeId, Account user, Recipe recipeData)
    {
        Recipe foundRecipe = GetRecipeById(recipeId);
        if (foundRecipe.CreatorId != user.Id)
        {
            throw new Exception("Not your recipe");
        }
        foundRecipe.Title = recipeData.Title ?? foundRecipe.Title;
        foundRecipe.Instructions = recipeData.Instructions ?? foundRecipe.Instructions;
        foundRecipe.Img = recipeData.Img ?? foundRecipe.Img;
        foundRecipe.Category = recipeData.Category ?? foundRecipe.Category;
        Recipe recipe = _repository.UpdateRecipe(foundRecipe);
        return recipe;
    }
    internal string RemoveRecipe(int recipeId, string userId)
    {
        Recipe foundRecipe = GetRecipeById(recipeId);
        if (foundRecipe.CreatorId != userId)
        {
            throw new Exception("The user doesn't have the same id as the creator of this recipe");
        }
        _repository.RemoveRecipe(recipeId);
        return "Recipe has been deleted";
    }
}