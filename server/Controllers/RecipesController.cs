namespace real_all_spice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController(Auth0Provider auth0Provider, RecipesService recipesService, IngredientsService ingredientsService) : ControllerBase
    {
        private readonly Auth0Provider _auth0Provider = auth0Provider;
        private readonly RecipesService _recipesService = recipesService;
        private readonly IngredientsService _ingredientsService = ingredientsService;

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Recipe>> AddRecipe([FromBody] Recipe recipeData)
        {
            try
            {
                Account userId = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                recipeData.CreatorId = userId.Id;
                Recipe recipe = _recipesService.AddRecipe(recipeData);
                return recipe;
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [HttpGet]
        public ActionResult<List<Recipe>> GetRecipes()
        {
            try
            {
                List<Recipe> recipes = _recipesService.GetRecipes();
                return recipes;
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [HttpGet("{recipeId}")]
        public ActionResult<Recipe> GetRecipeById(int recipeId)
        {
            try
            {
                Recipe recipe = _recipesService.GetRecipeById(recipeId);
                return recipe;
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [Authorize]
        [HttpPut("{recipeId}")]
        public async Task<ActionResult<Recipe>> UpdateRecipe(int recipeId, [FromBody] Recipe recipeData)
        {
            try
            {
                Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                Recipe recipe = _recipesService.UpdateRecipe(recipeId, user, recipeData);
                return recipe;
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [Authorize]
        [HttpDelete("{recipeId}")]
        public async Task<ActionResult<string>> RemoveRecipe(int recipeId)
        {
            try
            {
                Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                string userId = user.Id;
                string res = _recipesService.RemoveRecipe(recipeId, userId);
                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [HttpGet("{recipeId}/ingredients")]
        public ActionResult<List<Ingredient>> GetRecipeIngredients(int recipeId)
        {
            try
            {
                List<Ingredient> ingredients = _ingredientsService.GetRecipeIngredients(recipeId);
                return Ok(ingredients);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}