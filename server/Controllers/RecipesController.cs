using Microsoft.AspNetCore.Http.HttpResults;

namespace real_all_spice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController(Auth0Provider auth0Provider, RecipesService recipesService) : ControllerBase
    {
        private readonly Auth0Provider _auth0Provider = auth0Provider;
        private readonly RecipesService _recipesService = recipesService;

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
                return BadRequest(error);
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
                return BadRequest(error);
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
                return BadRequest(error);
            }
        }
    }
}