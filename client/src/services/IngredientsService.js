import { AppState } from "../AppState"
import { Ingredient } from "../models/Ingredient"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class IngredientsService {
    async getIngredients(recipeId) {
        try {
            const res = await api.get(`api/recipes/${recipeId}/ingredients`)
            AppState.ingredients = res.data.map(ingredient => new Ingredient(ingredient))
        } catch (error) {
            logger.error(error)
        }
    }
}

export const ingredientsService = new IngredientsService()