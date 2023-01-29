<template>

  <div @click="setActiveKeep" data-bs-toggle="modal" data-bs-target="#activeKeepModal"
    class="card bg-dark text-white border border-0 elevation-5 m-3 hover ">

    <img :src="keep.img" class="card-img" alt="...">
    <div class="card-img-overlay d-flex align-items-end justify-content-between info">
      <img :src="keep.creator.picture" height="40" width="40"
        class="rounded-circle elevation-2 profilePic border border-2 border-solid border-dark" alt=""
        :title="keep.creator.name" :aria-label="keep.creator.name">
      <div class="card-title  fs-3 fw-bold text-lowercase  " data-bs-toggle="modal" data-bs-target="#exampleModal">
        <span :title="keep.name" :aria-label="keep.name" class="hovering">{{ keep.name }}</span>
      </div>

      <div @click="deleteKeep" v-if="keep.creatorId == account.id" class=" text-end hovering">
        <i class="mdi mdi-trash-can fs-5 remove rounded-5 px-1 hoverable"></i>
      </div>
    </div>
  </div>





</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { keepsService } from "../services/KeepsService.js";
import Pop from "../utils/Pop.js";
export default {
  props: {
    keep: { Type: Object, required: true }
  },
  setup(props) {
    return {
      account: computed(() => AppState.account),

      async deleteKeep() {
        try {
          const yes = await Pop.confirm();
          if (!yes) {
            return;
          }
          await keepsService.deleteKeep(props.keep.id)
          Pop.success("removed keep!")
        } catch (error) {
          Pop.error(error)
        }
      },
      async setActiveKeep() {
        try {
          await keepsService.setActiveKeep(props.keep.id)
        } catch (error) {
          Pop.error(error)
        }
      },

    }
  }
};
</script>


<style lang="scss" scoped>
.remove {
  color: rgba(0, 0, 0, 0.279);
  background-color: rgba(255, 255, 255, 0.284);
}

.remove:hover {
  color: rgb(255, 0, 0);
  background-color: white;
  transition: all 0.7s ease;
}

.hovering {
  opacity: 0;
}

.profilePic {
  opacity: 100;
}

.info {
  display: none;
}

.info:hover {
  .hovering {
    opacity: 1;
    transition: all 1.0s ease;
  }

  .profilePic {
    opacity: 1;
    transition: all 1.5s ease;
  }

}

@media screen AND (max-width: 768px) {
  .hovering {
    font-size: small;
  }

  .profilePic {
    display: none;
  }
}
</style>