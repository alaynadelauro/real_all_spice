namespace real_all_spice.Services;
public class IngredientsService(IngredientsRepository ingredientsRepository)
{
    private readonly IngredientsRepository _repo = ingredientsRepository;
    internal Ingredient AddIngredient(Ingredient ingredientData)
    {
        Ingredient ingredient = _repo.AddIngredient(ingredientData);
        return ingredient;
    }
}