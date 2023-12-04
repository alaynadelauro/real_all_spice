<template>
    <div v-if="favorites" class="row mt-5 justify-content-center">
        <div class="col-9">
            <h3 class="favorite-card rounded-pill p-2 ps-5"> {{ account.name }}'s favorites: </h3>
            <div class="row justify-content-evenly height-and-scroll">
                <div v-for="favorite in favorites" :key="favorite.favoriteId" class=" m-1 p-2 rounded col-md-3 col-9 favorite-card">
                    <img :src="favorite.img" class="favorite-img rounded w-100" />
                    <h3 class="text-center">{{ favorite.title }}</h3>
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

export default {
    setup() {
        return {
            account: computed(() => AppState.account),
            favorites: computed(() => AppState.favorites)
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