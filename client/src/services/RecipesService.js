import { AppState } from "../AppState"
import { Recipe } from "../models/Recipe"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
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
    async deleteRecipe(recipeId) {
        try {
            await api.delete(`api/recipes/${recipeId}`)
            AppState.recipes = AppState.recipes.filter(recipe => recipe.id != recipeId)
        } catch (error) {
            logger.error(error)
        }
    }
    async updateRecipe(recipeId, recipeData) {
        try {
            const res = await api.put(`api/recipes/${recipeId}`, recipeData)
            AppState.activeRecipe = new Recipe(res.data)
            AppState.recipes.splice(recipe => recipe.id != AppState.activeRecipe.id, 1)
            AppState.recipes.unshift(new Recipe(res.data))
        } catch (error) {
            logger.error(error)
        }
    }
    async createRecipe(recipeData) {
        try {
            const res = await api.post(`api/recipes`, recipeData)
            AppState.recipes.unshift(new Recipe(res.data))
            AppState.activeRecipe = new Recipe(res.data)
        } catch (error) {
            logger.error(error)
        }
    }
    async getRecipeById(recipeId) {
        try {
            const res = await api.get(`api/recipes/${recipeId}`)
            AppState.activeRecipe = new Recipe(res.data)
        } catch (error) {
            logger.error(error)
        }
    }
}
export const recipesService = new RecipesService()