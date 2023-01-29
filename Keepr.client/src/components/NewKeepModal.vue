<template>

  <div class="modal fade" id="keepForm" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="exampleModalLabel">Add your keep</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="createKeep()">
            <div class="mb-2">
              <input class="form-control " placeholder="name" type="text" v-model="editable.name" required>
            </div>
            <div class="mb-2">
              <input class="form-control" placeholder="image URL..." type="url" v-model="editable.img" required>
            </div>
            <div>
              <textarea placeholder="Keep Description" class="form-control mb-3" rows="8" v-model="editable.description"
                required></textarea>
            </div>

            <button class="mt-2 btn btn-dark" type="submit" data-bs-dismiss="modal">create</button>

          </form>
        </div>
        <div class="modal-footer">
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { keepsService } from "../services/KeepsService.js";
import Pop from "../utils/Pop.js";
import { Modal } from "bootstrap";
export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async createKeep() {
        try {
          const keep = await keepsService.createKeep(editable.value)
          editable.value = {}
          Modal.getOrCreateInstance('#exampleModal').hide()
        } catch (error) {
          Pop.error(error)
        }
      }
    }
  }
};
</script>


<style lang="scss" scoped>

</style>