<template>
  <div class=" row container-fluid px-3" v-if="profile">

    <div class="col-12 pt-3">
      <div class=" img-fluid img-vfx rounded-3 pt-1 " :style="{ backgroundImage: `url(${profile?.coverImg})` }">
        <div class="col-md-12 d-flex justify-content-center">
          <div class="pt-1">
            <img :src="profile?.picture" alt="" class="rounded-circle p-4 img-fluid img-size px-5" />
          </div>
        </div>
      </div>
    </div>
    <div class="row justify-content-center align-items-center   mb-1">
      <div class="col-md text-center pt-2">
        <h1 class="username mt-4 fw-bold">{{ profile?.name }}</h1>
        <div class="fw-bold"> {{ keeps?.length }} Keeps | | {{ vaults.length }} Vaults </div>
      </div>
    </div>

    <div>
      <h1>Keeps</h1>
      <div class="masonrycol mb-3">
        <div class="" v-for="k in keeps">
          <KeepCard :keep="k" />
        </div>
      </div>
      <h1>vaults</h1>
      <div class="masonrycol mb-3">
        <div class="">
          <div class="" v-for="v in vaults">
            <VaultCard :vault="v" />
          </div>
        </div>
      </div>
    </div>
  </div>
  <div v-else class=" container-fluid">
    <div class="row p-5">
      <div class="col-12 text-center p-5">
        <i class="mdi mdi-loading mdi-spin fs-1"></i>
      </div>
    </div>
  </div>

</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { useRoute } from "vue-router";
import { profileService } from "../services/ProfileService.js"
import Pop from "../utils/Pop.js";
import VaultCard from "../components/VaultCard.vue";
import KeepCard from "../components/KeepCard.vue";
export default {
  setup() {
    const route = useRoute();
    async function getProfileById() {
      try {
        await profileService.getProfileById(route.params.id);
      }
      catch (error) {
        Pop.error(error, "");
      }
    }
    async function getProfilesKeeps() {
      try {
        await profileService.getProfilesKeeps(route.params.id);
      }
      catch (error) {
        Pop.error(error);
      }
    }
    async function getProfilesVaults() {
      try {
        await profileService.getProfilesVaults(route.params.id);
      }
      catch (error) {
        Pop.error(error);
      }
    }
    onMounted(() => {
      getProfileById();
      getProfilesKeeps();
      getProfilesVaults();
    });
    return {
      account: computed(() => AppState.account),
      profile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.profilesKeeps),
      vaults: computed(() => AppState.profileVaults),
    };
  },
  components: { VaultCard, KeepCard }
};
</script>


<style lang="scss" scoped>
.img-size {
  height: 30vh;
}


.vault-vfx {
  height: 10vh;
}


.masonrycol {
  columns: 3;
}

@media screen AND (max-width: 768px) {
  .masonrycol {
    columns: 2;
  }


  .img-vfx {
    background-position: center;
    background-repeat: no-repeat;
  }






}
</style>