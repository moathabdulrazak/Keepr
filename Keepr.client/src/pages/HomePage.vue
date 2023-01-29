<template>
  <!-- <h1>homepage</h1> -->
  <NewKeepModal />
  <div>
    <ActiveKeepModal />
  </div>
  <div class="row container-fluid d-flex">
    <div class="col-md-4 col-5 p-1  align-items-center" v-for="k in keeps">
      <KeepCard :keep="k" />
    </div>
  </div>
</template>

<script>
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js"
import { onMounted, computed, ref } from "vue";
import KeepCard from "../components/KeepCard.vue";
import { AppState } from "../AppState.js";
import NewKeepModal from "../components/NewKeepModal.vue";
import ActiveKeepModal from "../components/ActiveKeepModal.vue";
export default {
  setup() {
    async function getKeeps() {
      try {
        await keepsService.getKeeps();
      }
      catch (error) {
        Pop.error(error);
      }
    }
    onMounted(() => {
      getKeeps();
    });
    return {
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account),
      activeKeeps: computed(() => AppState.activeKeep)
    };
  },
  components: { KeepCard, NewKeepModal, ActiveKeepModal }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: 50vw;

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
