<template>
  <nav class="container-fluid bg-dark">
    <div class="row justify-content-between align-items-center">
      <div class="col-3">
        <router-link :to="{ name: 'Home' }">
          <h2 class="text-light imported-font mb-0">All Spice</h2>
        </router-link>
      </div>
      <div class="col-3 pt-3 text-end" id="navbarText">
        <!-- LOGIN COMPONENT HERE -->
        <Login />
      </div>
    </div>

  </nav>
</template>

<script>
import { onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
export default {
  setup() {

    const theme = ref(loadState('theme') || 'light')

    onMounted(() => {
      document.documentElement.setAttribute('data-bs-theme', theme.value)
    })

    return {
      theme,
      toggleTheme() {
        theme.value = theme.value == 'light' ? 'dark' : 'light'
        document.documentElement.setAttribute('data-bs-theme', theme.value)
        saveState('theme', theme.value)
      }
    }
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.imported-font {
  font-family: 'potta one';
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
