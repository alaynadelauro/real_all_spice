<template>
    <div class="row mt-5 justify-content-center">
        <div v-if="recipes.length >= 1" class="col-9">
            <div class="d-flex justify-content-between favorite-card rounded-pill p-2 px-5">
                <h3>Recipes Created By {{ account.name }}: </h3>
                <button class="btn btn-success text-light" @click="summonCreateModal()">Create</button>
            </div>
            <div class="row justify-content-evenly text-center height-and-scroll">
                <div class="rounded col-md-3 col-9 favorite-card m-1 p-2" v-for="recipe in recipes" :key="recipe.id">
                    <img type="button" @click="summonDetailModal(recipe.id)" :src="recipe.img" class="recipe-img rounded w-100" />
                    <h3>{{ recipe.title }}</h3>
                    <div class="d-flex justify-content-between">
                        <button class="btn btn-info" title="edit recipe" @click="summonEditModal(recipe.id)"><i class="mdi mdi-pen"></i></button>
                        <button class="btn btn-danger" title="delete recipe" @click="deleteRecipe(recipe.id)"><i class="mdi mdi-delete"></i></button>
                    </div>
                </div>
            </div>
        </div>
        <div v-else class="col-9 favorite-card rounded-pill p-2 px-5 d-flex justify-content-between">
            <h3>{{ account.name }} has not created any recipes</h3>
            <button class="btn btn-success text-light" @click="summonCreateModal()">Change That</button>
        </div>
    </div>
</template>


<script>
import { computed } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { recipesService } from '../services/RecipesService';
import { Modal } from 'bootstrap';

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
            },
            async summonDetailModal(recipeId) {
                try {
                    await recipesService.getRecipeById(recipeId)
                    Modal.getOrCreateInstance('#activeRecipeModal').show()
                } catch (error) {
                    Pop.error(error)
                }
            },
            async summonEditModal(recipeId) {
                try {
                    await recipesService.getRecipeById(recipeId)
                    Modal.getOrCreateInstance('#editRecipeModal').show()
                } catch (error) {
                    Pop.error(error)
                }
            },
            summonCreateModal() {
                Modal.getOrCreateInstance('#createRecipeModal').show()
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