namespace real_all_spice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientsController(Auth0Provider auth0Provider, IngredientsService ingredientsService) : ControllerBase
    {
        private readonly Auth0Provider _auth0Provider = auth0Provider;
        private readonly IngredientsService _ingredientService = ingredientsService;
        [Authorize]
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
    }
}