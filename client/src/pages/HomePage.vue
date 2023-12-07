<template>
  <div class="container-fluid">
    <div class="row justify-content-center">
      <div class="col-5 title-card rounded-pill mt-3 p-3 text-center">
        <h1>All Spice</h1>
      </div>
      <div class="row justify-content-center title-card rounded-pill text-center mt-5 p-3">
        <div class="col-md-2 col-9" v-for="category in categories" :key="category" type="button">
          <p class="mb-0 title-card rounded-pill text-center m-1 p-2" @click="changeCategory(category)">{{ category }}</p>
        </div>
      </div>
      <div class="row mt-3 justify-content-evenly" v-if="recipes">
        <div class="col-md-3 col-sm-9 m-1 title-card rounded m-1 p-2" v-for="recipe in recipes" :key="recipe.id" @click="SetActiveRecipe(recipe.id)" type="button" data-bs-toggle="modal" data-bs-target="#activeRecipeModal">
          <p class="mb-0 text-center">{{ recipe.title }}</p>
          <img :src="recipe.img" class="recipe-img w-100">
          <p class="text-center">{{ recipe.category }}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, ref } from 'vue';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { AppState } from '../AppState.js';
import { recipesService } from '../services/RecipesService.js';
import { Modal } from 'bootstrap';
import { favoritesService } from '../services/FavoritesService';
import { ingredientsService } from '../services/IngredientsService';
import { Favorite } from '../models/Favorite';

export default {
  setup() {
    const categories = ["All", "Cheese", "Italian", "Soup", "Mexican", "Specialty Coffee"];
    const filteredCategory = ref("");
    // NOTE moved this to the AppState
    // let recipeFavorited = false
    onMounted(() => {
      getRecipes()
    })
    async function getRecipes() {
      try {
        await recipesService.getRecipes()
        // logger.log(AppState.recipes)
      } catch (error) {
        Pop.error(error)
        logger.error(error)
      }
    }
    return {
      categories,
      // recipeFavorited,
      recipes: computed(() => {
        if (filteredCategory.value) {
          return AppState.recipes.filter((recipe) => recipe.category == filteredCategory.value)
        } else { return AppState.recipes }
      }),
      account: computed(() => AppState.account),
      changeCategory(category) {
        logger.log("button clicked", filteredCategory.value)
        filteredCategory.value = category
        if (filteredCategory.value == "All") {
          filteredCategory.value = null
          return
        }
      },
      async SetActiveRecipe(recipeId) {
        try {
          const foundRecipe = AppState.recipes.find(recipe => recipe.id == recipeId)
          if (!foundRecipe) {
            Pop.error("something went wrong, recipe was not found")
          } else {
            AppState.activeRecipe = foundRecipe
            ingredientsService.getIngredients(recipeId)
            if (AppState.account.id) {
              await favoritesService.getFavorites()
              // debugger
              const foundFavorite = AppState.favorites.find(favorite => favorite.id == recipeId)
              if (foundFavorite) {
                AppState.recipeFavorited = true
                logger.log("this was favorited")
              } else {
                AppState.recipeFavorited = false
                logger.log("this was not")
              }
            }
          }
          // logger.log(AppState.activeRecipe)
        } catch (error) {
          Pop.error(error)
          logger.error(error)
        }
      }
    }
  }
}
</script>

<style scoped lang="scss">
.title-card {
  background-color: rgba(245, 245, 245, 0.385);
  font-family: 'potta one';
  box-shadow: 5px 4px 7px blue;
}

.recipe-img {
  object-fit: cover;
  object-position: center;
  max-height: 10rem;
}

* {
  user-select: none;
}
</style>
