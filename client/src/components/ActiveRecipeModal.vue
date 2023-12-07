<template>
    <div class="modal fade modal-lg" id="activeRecipeModal" tabindex="-1" aria-labelledby="activeRecipeModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div v-if="ingredients" class="modal-content">
                <div v-if="recipe" class="active-recipe-img modal-header" :style="{ backgroundImage: `url(${recipe.img})` }">
                    <h1 class="modal-title modal-title-personalized rounded-pill px-3" id="activeRecipeModalLabel">{{ recipe.title }}</h1>
                    <button type="button" class="btn text-light fs-1" data-bs-dismiss="modal" aria-label="Close"><i class="mdi mdi-close"></i></button>
                </div>
                <div v-if="recipe.creator" class="modal-body">
                    <p class="fs-5 text-center">Recipe for {{ recipe.category }}</p>
                    <p class="fs-5">Instructions:</p>
                    <p>{{ recipe.instructions }}</p>
                    <p class="fs-5">Ingredients:</p>
                    <div v-for="ingredient in ingredients">
                        <p>{{ ingredient.quantity }} of {{ ingredient.name }}</p>
                    </div>
                    <div class="d-flex align-items-center justify-content-end">
                        <img :src="recipe.creator.picture" class="creator-picture rounded-circle me-3">
                        <p class="text-end mb-0">This recipe brought to you by {{ recipe.creator.name }}</p>
                    </div>
                    <div class="d-flex justify-content-end">
                        <p v-if="recipe.createdAt" class="pe-4"><span class="fw-bold">Created At:</span><br>{{ recipe.createdAt }}</p>
                        <p v-if="recipe.updatedAt"><span class="fw-bold">Last Updated:</span> <br>{{ recipe.updatedAt }}</p>
                    </div>
                </div>
                <div v-if="account.id && account.id != recipe.creatorId" class="modal-footer d-flex justify-content-between">
                    <div>
                        <button v-if="favorited == false" class="btn text-danger fs-4" title="Add to favorites" @click="favoriteRecipe(recipe.id)"><i class="mdi mdi-heart"></i></button>
                        <button v-if="favorited == true" class="btn text-danger fs-4" title="Remove from favorites" @click="unfavorite(favorite.favoriteId)"><i class="mdi mdi-heart-broken"></i></button>
                    </div>
                    <div>
                        <button v-if="recipe.creatorId == account.id" type="button" class="btn btn-success text-light me-3" @click="changeModal()">Edit</button>
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    </div>
                </div>
                <div v-else-if="account.id && account.id == recipe.creatorId" class="modal-footer d-flex justify-content-end">
                    <button v-if="recipe.creatorId == account.id" type="button" class="btn btn-success text-light me-3" @click="changeModal()">Edit</button>
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                </div>
                <div v-else-if="!account.id" class="modal-footer d-flex justify-content-end">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { computed, onMounted, onUnmounted, watch, watchEffect } from 'vue';
import { AppState } from '../AppState';
import { Modal } from 'bootstrap';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { favoritesService } from '../services/FavoritesService.js';
import { ingredientsService } from '../services/IngredientsService.js'

export default {
    setup() {
        onMounted(() => {
            checkRecipeFavorite()
        })
        // NOTE this is on homepage set active recipe
        // onMounted(() => {
        //     getIngredients()
        // })
        // async function getIngredients() {
        //     try {
        //         const recipeId = AppState.activeRecipe.id
        //         await ingredientsService.getIngredients(recipeId)
        //     } catch (error) {
        //         Pop.error(error)
        //         logger.error(error)
        //     }
        // }
        async function checkRecipeFavorite() {
            if (AppState.account == {}) { return }
            const favorite = AppState.favorites.find(favorite => favorite.id == AppState.activeRecipe.id)
            if (favorite) {
                AppState.recipeFavorited == true
            } else { AppState.recipeFavorited == false }
        }
        return {
            recipe: computed(() => AppState.activeRecipe),
            account: computed(() => AppState.account),
            ingredients: computed(() => AppState.ingredients),
            favorited: computed(() => AppState.recipeFavorited),
            favorite: computed(() => AppState.favorites),
            changeModal() {
                Modal.getOrCreateInstance('#activeRecipeModal').hide()
                Modal.getOrCreateInstance('#editRecipeModal').show()
            },
            async favoriteRecipe(recipeId) {
                try {
                    // NOTE check to see where that function was coming from - it's in homepage
                    // logger.log('modal getting favorites')
                    // debugger
                    await favoritesService.favoriteRecipe(recipeId)
                    Modal.getOrCreateInstance('#activeRecipeModal').hide()
                    Modal.getOrCreateInstance('#activeRecipeModal').show()
                } catch (error) {
                    Pop.error(error)
                    logger.error(error)
                }
            },
            async unfavorite(favoriteId) {
                try {
                    const yes = await Pop.confirm("Remove from favorites?")
                    if (!yes) { return }
                    // debugger
                    const foundFavorite = await AppState.favorites.find(favorite => favorite.favoriteId == favoriteId)
                    if (!foundFavorite) { return } else {
                        await favoritesService.unfavorite(favoriteId)
                    }
                    Modal.getOrCreateInstance('#activeRecipeModal').hide()
                    Modal.getOrCreateInstance('#activeRecipeModal').show()
                } catch (error) {
                    Pop.error(error)
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped>
.active-recipe-img {
    background-position: center;
    background-size: cover;
    box-shadow: 0px 5px 4px blue;
}

.modal-title-personalized {
    background-color: rgba(255, 255, 255, 0.656);
    box-shadow: 0px 5px 4px blue;
}

.creator-picture {
    height: 7dvh;
    aspect-ratio: 1/1;
    object-fit: cover;
    object-position: center;
    ;
}
</style>