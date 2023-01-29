<template>
  <header>
    <Navbar />
  </header>
  <main class="bg-color">
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

.bg-color {
  background-color: #FEF6F0;
}

footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
