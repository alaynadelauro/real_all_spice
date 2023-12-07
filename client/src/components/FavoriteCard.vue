<template>
    <div v-if="favorites" class="row mt-5 justify-content-center">
        <div class="col-9">
            <h3 class="favorite-card rounded-pill p-2 ps-5"> {{ account.name }}'s favorites: </h3>
            <div class="row justify-content-evenly height-and-scroll">
                <div v-for="favorite in favorites" :key="favorite.favoriteId" class=" m-1 p-2 rounded col-md-3 col-9 favorite-card">
                    <img @click="summonDetailModal(favorite.id)" :src="favorite.img" class="favorite-img rounded w-100" />
                    <div class="d-flex justify-content-between align-items-center">
                        <h5 class="text-center mb-0">{{ favorite.title }}</h5>
                        <button class="btn text-danger" title="unfavorite" @click="unfavorite(favorite.favoriteId)"><i class="mdi mdi-heart-broken"></i></button>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <div v-else-if="!favorites" class="row justify-content-center mt-5 text-center">
        <h1><i class="mdi mdi-loading mdi-spin"></i></h1>
    </div>
</template>


<script>
import { computed, onMounted } from 'vue';
import { favoritesService } from '../services/FavoritesService';
import { AppState } from '../AppState.js';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { Modal } from 'bootstrap';
import { recipesService } from '../services/RecipesService';

export default {
    setup() {
        return {
            account: computed(() => AppState.account),
            favorites: computed(() => AppState.favorites),
            async unfavorite(favoriteId) {
                try {
                    const yes = await Pop.confirm("Are you sure you don't like this recipe anymore?")
                    if (!yes) { return }
                    await favoritesService.unfavorite(favoriteId)
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
            }
        }
    }
}
</script>


<style lang="scss" scoped>
.favorite-img {
    height: 10rem;
    aspect-ratio: 1/1;
    object-fit: cover;
    object-position: center;
}

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
</style>