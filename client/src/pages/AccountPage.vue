<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12 p-3 d-flex align-items-center">
        <img :src="account.picture" class="me-3 rounded-circle account-image">
        <h3 class="imported-font">{{ account.name }}</h3>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from 'vue';
import { AppState } from '../AppState';
export default {
  setup() {
    return {
      account: computed(() => AppState.account)
    }
  }
}
</script>

<style scoped>
.imported-font {
  font-family: 'Potta One';
}

.account-image {
  object-fit: cover;
  object-position: center;
}
</style>
