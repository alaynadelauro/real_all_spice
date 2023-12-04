<template>
  <div v-if="account" class="container-fluid">
    <div class="row justify-content-center">
      <div class="col-9 p-3 d-flex flex-column align-items-center account-info-card rounded-pill">
        <img :src="account.picture" class="me-3 rounded-circle account-image">
        <div class="d-flex align-items-center">
          <h1 class="imported-font mt-3">{{ account.name }}</h1>
          <button type="button" class="btn fs-3 mt-3" data-bs-toggle="modal" data-bs-target="#accountModal" title="Edit Account Details"><i class="mdi mdi-pen"></i></button>
        </div>
      </div>
    </div>
    <FavoriteCardVue />
  </div>
  <div v-else-if="!account" class="container-fluid">
    <h1>Loading <i class="mdi mdi-loading mdi-spin"></i></h1>
  </div>
  <!-- SECTION modal -->
  <div class="modal fade" id="accountModal" tabindex="-1" aria-labelledby="accountModal" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="accountModalLabel">Edit Account</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="EditAccount()">
            <div>
              <label for="name" class="form-label">Name</label>
              <input v-model="editable.name" maxlength="255" type="text" class="form-control" :placeholder="account.name">
            </div>
            <div>
              <label for="picture" class="form-label">Picture</label>
              <input v-model="editable.picture" maxlength="255" type="url" class="form-control" :placeholder="account.picture">
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
              <button type="submit" class="btn btn-primary">Save changes</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, ref } from 'vue';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { accountService } from '../services/AccountService';
import { Modal } from 'bootstrap';
import FavoriteCardVue from '../components/FavoriteCard.vue'
import { favoritesService } from '../services/FavoritesService.js';
export default {
  setup() {
    const editable = ref({})
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
    }
    return {
      editable,
      account: computed(() => AppState.account),
      favorites: computed(() => AppState.favorites),
      async EditAccount() {
        try {
          const accountId = AppState.account.id
          await accountService.editAccount(accountId, editable.value)
          editable.value = {}
          Modal.getOrCreateInstance("#accountModal").hide()
        } catch (error) {
          Pop.error(error)
          logger.error(error)
        }
      }
    }
  },
  components: { FavoriteCardVue }
}
</script>

<style scoped>
.imported-font {
  font-family: 'Potta One';
}

.account-image {
  object-fit: cover;
  object-position: center;
  max-height: 10rem;
  aspect-ratio: 1/1;
}

.account-info-card {
  background-color: rgba(245, 245, 245, 0.385);
}
</style>
