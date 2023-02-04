<template>
  <div class="component" v-if="user.isAuthenticated">
    <div>
      <div class="text-dark">Add to vault..</div>
    </div>
    <form @submit.prevent="createKeepForVault()" class="d-flex justify-content-center align-items-center border-0">
      <div class="dropDown" height="30" width="30">
        <select v-model="editable" class="form-select  border border-dark border-3" required title="Add to vault"
          aria-label="Select vault..">
          <option v-for="v in vaults" :value="v" required selected><a class="dropdown-item">{{ v.name }}</a></option>
        </select>

      </div>
      <div class="ms-2">
        <button class="btn p-0 px-1 bg-c6 mt-1 add " type="submit">
          <i class="fs-5 add mdi mdi-plus-circle-outline fs-4 text-dark"></i>
        </button>
      </div>
    </form>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { vaultsKeepsService } from "../services/VaultKeepsService.js";
import Pop from "../utils/Pop.js";
export default {
  setup() {
    const editable = ref({});
    return {
      vaults: computed(() => AppState.myAccountVaults),
      user: computed(() => AppState.user),
      activeKeep: computed(() => AppState.activeKeep),
      editable,
      async createKeepForVault() {
        let keepnvaultid = {
          keepId: AppState.activeKeep.id,
          vaultId: editable.value.id
        }
        try {
          Pop.success(
            `Added to vault`
          );
          await vaultsKeepsService.createVaultKeep(keepnvaultid)
          editable.value = {}
        } catch (error) {
          Pop.error(error)
        }
      },

    }
  }
}
</script>


<style lang="scss" scoped>

</style>