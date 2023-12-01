namespace real_all_spice.Models;
public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Quantity { get; set; }
    public int RecipeId { get; set; }
}