<template>
  <router-link :to="{ name: 'Vault', params: { id: vault.id } }" class="text-white">
    <div class="  justify-content-center card bg-dark text-white border border-0 elevation-3  m-3  vaultcard"
      :style="{ backgroundImage: `url(${vault?.img})` }" :title="vault.name" :aria-label="vault.name">
      <!-- <img :src="vault.img" class="card-img img-fluid" alt="..."> -->
      <div class="card-img-overlay d-flex align-items-end justify-content-between">
        <div class="fs-3 fontbg text-uppercase">
          {{ vault.name }}</div>
        <div v-if="vault.isPrivate"><i class="mdi mdi-lock fs-2" title="Private Vault"></i></div>

      </div>
    </div>

  </router-link>
  <div title="trash vault?" @click="deleteVault" v-if="vault.creatorId == account.id" class=" text-end ">
    <i class="mdi mdi-trash-can fs-5  rounded-5 px-1 hovering "></i>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { vaultsService } from "../services/VaultsService.js";
import Pop from "../utils/Pop.js";
export default {
  props: {
    vault: {
      type: Object,
      required: true,
    }
  },
  setup(props) {
    return {
      account: computed(() => AppState.account),
      async deleteVault() {
        try {
          const yes = await Pop.confirm();
          if (!yes) {
            return;
          }
          await vaultsService.deleteVault(props.vault.id)
          Pop.success("removed vault!")
        } catch (error) {
          Pop.error(error)
        }
      },
    }
  }
};
</script>


<style lang="scss" scoped>
.vaultcard {
  height: 190px;
  background-position: center;
  background-size: cover;
}

.hovering {
  cursor: pointer;
}


@media screen AND (max-width: 768px) {}
</style>