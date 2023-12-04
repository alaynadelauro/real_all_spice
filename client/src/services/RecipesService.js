import { AppState } from "../AppState"
import { Recipe } from "../models/Recipe"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class RecipesService {
    async getRecipes() {
        try {
            const res = await api.get('api/recipes')
            AppState.recipes = res.data.map((recipe) => new Recipe(recipe))
        } catch (error) {
            logger.error(error)
        }
    }
}
export const recipesService = new RecipesService()