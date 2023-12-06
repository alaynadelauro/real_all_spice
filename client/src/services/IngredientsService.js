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
    async addIngredient(ingredientData) {
        try {
            const res = await api.post(`api/ingredients`, ingredientData)
            AppState.ingredients.unshift(new Ingredient(res.data))
        } catch (error) {
            logger.error(error)
        }
    }
    async removeIngredient(ingredientId) {
        try {
            await api.delete(`api/ingredients/${ingredientId}`)
            AppState.ingredients = AppState.ingredients.filter(ingredient => ingredient.id != ingredientId)
        } catch (error) {
            logger.error(error)
        }
    }
}

export const ingredientsService = new IngredientsService()