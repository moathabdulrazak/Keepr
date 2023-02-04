<template>

  <EditAccountModal />

  <ActiveKeepModal />
  <div class="col-12 pt-3">
    <div class=" img-fluid img-vfx rounded-3 pt-1 " :style="{ backgroundImage: `url(${account?.coverImg})` }">
      <button type="button" class="btn mdi mdi-book-edit text-light " data-bs-toggle="modal"
        data-bs-target="#accountModal">
        mange account
      </button>
      <div class="about text-center  text-white fw-bold">
        <h1>Welcome {{ account.name }}</h1>
        <img class="rounded-acc" :src="account?.picture" alt="" />
        <p>{{ account.email }}</p>
        <div class="fw-bold  text-white fw-bold"> {{ keeps?.length }} Keeps | | Vaults {{ vaults.length }}</div>
      </div>
    </div>
  </div>

  <div class="row container-fluid">
    <h1 class="">Vaults</h1>
    <div class="col-md-3  " v-for="v in vaults">
      <VaultCard :vault="v" :key="v.id" />
    </div>
  </div>
  <div class="row container-fluid">
    <h1 class="">Keeps</h1>
    <div class="d-flex align-items-center col-md-3" v-for="k in keeps">
      <KeepCard :keep="k" :key="k.id" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted, ref } from 'vue'
import { AppState } from '../AppState'
import { accountService } from "../services/AccountService.js"
import Pop from "../utils/Pop.js"
import { profileService } from "../services/ProfileService.js"
import KeepCard from "../components/KeepCard.vue";
import VaultCard from "../components/VaultCard.vue";
import EditAccountModal from "../components/EditAccountModal.vue";
import { keepsService } from "../services/KeepsService.js"
import ActiveKeepModal from "../components/ActiveKeepModal.vue"
export default {

  setup(props) {
    const editable = ref({});
    async function getProfilesKeeps() {
      try {
        await keepsService.getKeeps();
      }
      catch (error) {
        Pop.error(error);
      }
    }
    async function getMyVaults() {
      try {
        await accountService.getMyVaults();
      }
      catch (error) {
        Pop.error;
      }
    }
    onMounted(() => {
      getProfilesKeeps();
      getMyVaults();
    });
    return {
      editable,
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps.filter(k => k.creatorId == AppState.account.id)),
      // profile: computed(() => AppState.activeProfile),
      vaults: computed(() => AppState.myAccountVaults),


    };
  },
  components: { KeepCard, VaultCard, EditAccountModal, ActiveKeepModal }
}
</script>

<style scoped>
img {
  max-width: 100px;
}

.rounded-acc {
  border-radius: 50%;
}
</style>
