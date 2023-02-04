<template>


  <div class="modal fade" id="accountModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="exampleModalLabel">Modal title</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <div class="row container-fluid">


            <div class="col-md-6 p-4 text-black modal-body">
              <form class=" account-form acc-bio p-3 rounded" @submit.prevent="editAccount()">
                <div class="acc-pic text-start">
                  <div>
                    <label for="name">Name:</label>
                    <input type="text" class="form-control" v-model="editable.name">
                  </div>
                  <div>
                    <label for="picture">Picture:</label>
                    <input type="url" class="form-control" v-model="editable.picture" name="picture"
                      placeholder="picture">
                  </div>
                  <div>
                    <label for="coverImg">Cover Image:</label>
                    <input type="url" class="form-control" v-model="editable.coverImg" name="coverImg">
                  </div>
                  <div class="col-md-2">
                    <button type="submit" class="btn btn-dark w-100 mt-2 ">Save</button>
                  </div>
                </div>
              </form>
            </div>
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
          <button type="button" class="btn btn-primary">Save changes</button>
        </div>
      </div>
    </div>
  </div>
</template>


<script>

import { Modal } from "bootstrap";
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { accountService } from "../services/AccountService.js";
import Pop from "../utils/Pop.js";
export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      account: computed(() => AppState.account),
      async editAccount() {
        try {
          debugger
          await accountService.editAccount(editable.value)
          Modal.getOrCreateInstance('#accountModal').hide()
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