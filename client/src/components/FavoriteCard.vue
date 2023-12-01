<template>
    <div v-if="favorites" class="row mt-5 justify-content-center">
        <div class="col-9">
            <h3> {{ account.name }}'s favorites: </h3>
        </div>
        <div v-for="favorite in favorites" :key="favorite.favoriteId" class="col-4">
            <img :src="favorite.img" />
            <h1>{{ favorite.title }}</h1>
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
        onMounted(() => {
            getFavorites()
        })
        async function getFavorites() {
            try {
                // debugger
                await favoritesService.getFavorites()
                logger.log(AppState.favorites)
            } catch (error) {
                Pop.error(error)
                logger.error(error)
            }
            return {
                favorites: computed(() => AppState.favorites),
                account: computed(() => AppState.account)
            }
        }
    }
}
</script>


<style lang="scss" scoped></style>