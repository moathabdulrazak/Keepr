<template>
  <nav class="navbar navbar-expand-lg nav-bar-color  px-4 elevation-2">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <button class="btn btn-color">Home</button>
    </router-link>
    <div class="dropdown">
      <button v-if="Account.id" class="btn dropdown-toggle" type="button" id="dropdownMenuButton1"
        data-bs-toggle="dropdown" aria-expanded="false">
        create
      </button>
      <ul class="dropdown-menu  dropdown-color" aria-labelledby="dropdownMenuButton1">
        <button data-bs-toggle="modal" data-bs-target="#keepForm" class="btn"> Create Keep</button>
        <button data-bs-toggle="modal" data-bs-target="#newVault" class="btn"> Create Vault</button>
      </ul>
    </div>
    <!-- <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button> -->
    <h1 class="text-center text-vfx col-9">Kee<span class="text-vfx2">p</span>r </h1>
    <!-- <div>

      <img class="img-fluid justify-content-center m-uto" src="src\assets\img\Keepr logo.png" alt="">
    </div> -->
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>

        </li>
      </ul>

      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
</template>

<script>
import { AppState } from "../AppState.js";
import Login from './Login.vue'
import { computed } from "@vue/reactivity";
import { Account } from "../models/Account.js";
export default {
  setup() {
    return {
      Account: computed(() => AppState.account)
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

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}

.dropdown-color {
  background-color: #DED6E9;
}

.nav-bar-color {
  background-color: #FEF6F0;
}

.btn-color {
  background-color: #E9D8D6;
}

.text-vfx2 {
  color:
    #7c4dbd;
}
</style>
