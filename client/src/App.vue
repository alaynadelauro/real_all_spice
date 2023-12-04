<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}

main {
  background-image: linear-gradient(white, blue);
}

::-webkit-scrollbar {
  background-color: blue;
}

::-webkit-scrollbar-thumb {
  border: 1px solid black;
  box-shadow: 0px 0px 2px inset black;
  border-radius: 10px;
  background-color: blue;
}
</style>
