<template>
  <!-- <h1>homepage</h1> -->
  <NewKeepModal />
  <div>
    <ActiveKeepModal />
  </div>
  <NewVaultModal />
  <div class=" msContainer px-5 pt-1">
    <div class=" col-md-3 col-6 p-1 " v-for="k in keeps">
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
import NewVaultModal from "../components/NewVaultModal.vue";
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
  components: { KeepCard, NewKeepModal, ActiveKeepModal, NewVaultModal }
}
</script>

<style scoped lang="scss">
.home {

  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;
}



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

.msContainer {
  columns: 4 200px;
  column-gap: 1rem;

}

.msContainer div {
  width: 150px;
  // margin: 0 1rem 1rem 0;
  display: inline-block;
  width: 100%;
  text-align: center;
  font-family: system-ui;
  font-weight: 900;
  font-size: 2rem;
}

@media screen AND (max-width: 768px) {
  .msContainer {
    columns: 2;
  }
}


//   .masonry-with-columns {
//   columns: 6 200px;
//   column-gap: 1rem;
// }
// .masonry-with-columns div {
//   width: 150px;
//   background: #EC985A;
//   color: white;
//   margin: 0 1rem 1rem 0;
//   display: inline-block;
//   width: 100%;
//   text-align: center;
//   font-family: system-ui;
//   font-weight: 900;
//   font-size: 2rem;
// }



// @for $i from 1 through 36 {
//   .msContainer:nth-child(#{$i}) {
//     $h: (random(400) + 100)+px;
//     height: $h;
//     line-height: $h;
//   }
// }
</style>
