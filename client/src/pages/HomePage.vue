<template>
  <div class="container-fluid">
    <div class="row justify-content-center">
      <div class="col-5 title-card rounded-pill mt-3 p-3 text-center">
        <h1>All Spice</h1>
      </div>
      <div class="row justify-content-center">
        <div class="col-9 title-card rounded-pill text-center mt-5 p-2">
          <p class="mb-0">Categories: cleese, italian, soup, mexican, specialty coffee</p>
        </div>
      </div>
      <div class="row mt-3 justify-content-center" v-if="recipes">
        <div class="col-3 m-1 title-card rounded m-1 p-2" v-for="recipe in recipes " :key="recipe.id">
          <img :src="recipe.img" class="recipe-img w-100">
          <p class="mb-0 text-center">{{ recipe.title }}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { AppState } from '../AppState.js';
import { recipesService } from '../services/RecipesService.js'

export default {
  setup() {
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
      recipes: computed(() => AppState.recipes)
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
</style>
