<template>
    <div class="modal fade modal-lg" id="activeRecipeModal" tabindex="-1" aria-labelledby="activeRecipeModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div v-if="recipe" class="active-recipe-img modal-header" :style="{ backgroundImage: `url(${recipe.img})` }">
                    <h1 class="modal-title modal-title-personalized rounded-pill px-3" id="activeRecipeModalLabel">{{ recipe.title }}</h1>
                    <button type="button" class="btn text-light fs-1" data-bs-dismiss="modal" aria-label="Close"><i class="mdi mdi-close"></i></button>
                </div>
                <div v-if="recipe.creator" class="modal-body">
                    <p class="fs-5 text-center">Recipe for {{ recipe.category }}</p>
                    <p class="fs-5">Instructions:</p>
                    <p>{{ recipe.instructions }}</p>
                    <p class="fs-5">Ingredients:</p>
                    <p>socks and shoes and centipedes</p>
                    <div class="d-flex align-items-center justify-content-end">
                        <img :src="recipe.creator.picture" class="creator-picture rounded-circle me-3">
                        <p class="text-end mb-0">This recipe brought to you by {{ recipe.creator.name }}</p>
                    </div>
                    <div class="d-flex justify-content-end">
                        <p v-if="recipe.createdAt" class="pe-4"><span class="fw-bold">Created At:</span><br>{{ recipe.createdAt }}</p>
                        <p v-if="recipe.updatedAt"><span class="fw-bold">Last Updated:</span> <br>{{ recipe.updatedAt }}</p>
                    </div>
                </div>
                <div class="modal-footer d-flex justify-content-between">
                    <div>
                        <button v-if="account.id" class="btn text-danger fs-4" title="favorite or unfavorite recipe. Warning: not reactive due to this being required to be from a modal.
                        to check if this worked, visit the account page to see if it's there." @click="favoriteRecipe(recipe.id)"><i class="mdi mdi-heart"></i></button>
                    </div>
                    <div>
                        <button v-if="recipe.creatorId == account.id" type="button" class="btn btn-success text-light me-3" @click="changeModal()">Edit</button>
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    </div>

                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { computed, onMounted, onUnmounted } from 'vue';
import { AppState } from '../AppState';
import { Modal } from 'bootstrap';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { favoritesService } from '../services/FavoritesService';

export default {
    setup() {
        return {
            recipe: computed(() => AppState.activeRecipe),
            account: computed(() => AppState.account),
            changeModal() {
                Modal.getOrCreateInstance('#activeRecipeModal').hide()
                Modal.getOrCreateInstance('#editRecipeModal').show()
            },
            async favoriteRecipe(recipeId) {
                try {
                    await favoritesService.favoriteRecipe(recipeId)
                } catch (error) {
                    Pop.error(error)
                    logger.error(error)
                }
            },
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