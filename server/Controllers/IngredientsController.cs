namespace real_all_spice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController(IngredientsService ingredientsService) : ControllerBase
{
    private readonly IngredientsService _ingredientService = ingredientsService;

    [HttpPost]
    public ActionResult<Ingredient> AddIngredient([FromBody] Ingredient ingredientData)
    {
        try
        {
            Ingredient ingredient = _ingredientService.AddIngredient(ingredientData);
            return Ok(ingredient);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
    [HttpDelete("{ingredientId}")]
    public ActionResult<string> RemoveIngredient(int ingredientId)
    {
        try
        {
            string res = _ingredientService.RemoveIngredient(ingredientId);
            return Ok(res);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
}

