<template>
    <div class="row mt-5 justify-content-center">
        <div class="col-9">
            <h3 class="favorite-card rounded-pill p-2 ps-5">Recipes Created By {{ account.name }}: </h3>
            <div class="row justify-content-evenly text-center height-and-scroll">
                <div class="rounded col-md-3 col-9 favorite-card m-1 p-2" v-for="recipe in recipes" :key="recipe.id">
                    <img :src="recipe.img" class="recipe-img rounded w-100" />
                    <h3>{{ recipe.title }}</h3>
                    <div class="d-flex justify-content-between">
                        <button class="btn btn-info" title="edit recipe"><i class="mdi mdi-pen"></i></button>
                        <button class="btn btn-danger" title="delete recipe" @click="deleteRecipe(recipe.id)"><i class="mdi mdi-delete"></i></button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { computed } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { recipesService } from '../services/RecipesService';

export default {
    setup() {
        return {
            account: computed(() => AppState.account),
            recipes: computed(() => AppState.recipes),
            async deleteRecipe(recipeId) {
                try {
                    const yes = await Pop.confirm("Are you sure you want to delete this recipe?")
                    if (!yes) { return }
                    await recipesService.deleteRecipe(recipeId)
                } catch (error) {
                    Pop.error(error)
                    logger.error(error)
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped>
.favorite-card {
    background-color: rgba(245, 245, 245, 0.385);
}

.height-and-scroll {
    max-height: 30rem;
    overflow-y: scroll;
}

::-webkit-scrollbar {
    background-color: rgba(255, 255, 255, 0);
}

.recipe-img {
    height: 10rem;
    aspect-ratio: 1/1;
    object-fit: cover;
    object-position: center;
}
</style>