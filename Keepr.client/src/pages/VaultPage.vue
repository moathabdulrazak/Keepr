<template>
  <div class="component">
    <ActiveVaultKeep />
    <div class="container" v-if="vault">
      <div class="row  justify-content-center">
        <div
          class="img-vfx rounded-2 mt-5 text-white fw-bold d-flex align-items-center justify-content-end flex-column "
          :style="{ backgroundImage: `url(${vault?.img})` }">
          <div v-if="vault.isPrivate" class="lock">
            <i class="mdi mdi-lock fs-2" title="Private Vault"></i>
          </div>
          <h1 class="text-center">{{ vault?.name }}</h1>
          <div v-if="account.id == vault.creatorId">
            <router-link :to="{ name: 'Account', params: { id: vault?.creator.id } }" class="text-white text-center">
              <p>by {{ vault?.creator.name }}</p>
            </router-link>
          </div>
          <div v-else>
            <!-- <router-link :to="{ name: 'Profile', params: { id: vault?.creator.id } }" class="text-white text-center">
              <p>created by {{ vault?.creator.name }}</p>
            </router-link> -->
          </div>

        </div>
        <div>
          <div class="d-flex justify-content-center pt-3">
            <div class="bg-grey py-2 px-3 rounded-pill">{{ keep.length }} keeps</div>
          </div>
          <div class="d-flex justify-content-center pt-3">
            <div class="bg-light py-2 px-3 rounded-3">{{ vault?.description }}</div>
          </div>
        </div>
      </div>
      <div class="pb-4 pt-4">
        <div class="masonry">
          <div class="d-flex align-items-center" v-for="k in keep">
            <KeepCard :keep="k" :key="k.id" />
          </div>
        </div>
      </div>
    </div>

  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { vaultsService } from "../services/VaultsService.js";
import Pop from "../utils/Pop.js";
import { useRoute, useRouter } from "vue-router";
import ActiveKeepModal from "../components/ActiveKeepModal.vue";
import { vaultsKeepsService } from "../services/VaultKeepsService.js";
import ActiveVaultKeep from "../components/ActiveVaultKeep.vue";
export default {
  setup() {
    const route = useRoute();
    const router = useRouter();
    async function getVaultById() {
      try {
        await vaultsService.getVaultById(route.params.id);
      }
      catch (error) {
        Pop.error("Private Vault ID");
        router.push({ name: "Home" });
      }
    }
    async function getKeepsByVaultId() {
      try {
        await vaultsService.getKeepsByVaultId(route.params.id);
      }
      catch (error) {
        Pop.error(error);
      }
    }
    onMounted(() => {
      getVaultById();
      getKeepsByVaultId();
    });
    return {
      vault: computed(() => AppState.activeVault),
      keep: computed(() => AppState.profilesKeeps),
      account: computed(() => AppState.account),

      async removeKeep() {
        try {
          if (await Pop.confirm("Unvault this keep?")) {
            await vaultsKeepsService.removeKeep(AppState.profilesKeeps.vaultKeepId, AppState.profilesKeeps.id)
          }
        } catch (error) {
          Pop.error(error)
        }
      },


    };
  },
  components: { ActiveKeepModal, ActiveVaultKeep }
};
</script>


<style lang="scss" scoped>
.img-vfx {
  background-size: cover;
  background-position: center;
  height: 250px;
  width: 650px;
}



.drpdwn {
  transform: translateY(3rem) translateX(19em);
}

.masonry {
  columns: 4;
}

.lock {
  transform: translateY(-6.5rem) translateX(19em);
}

.fs {
  font-size: 40px;
  font-weight: 700;
}

@media screen AND (max-width: 768px) {
  .masonry {
    columns: 2;
  }

  .drpdwn {
    transform: translateY(3rem) translateX(10em);
  }
}
</style>