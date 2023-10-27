<template>
  <div class="container form-class">
      <form @submit.prevent="editAccount">
        <div class="mb-3">
          <label for="name" class="form-label">Account Name</label>
          <input v-model="accountData.name" type="name" class="form-control" id="accountName"
            placeholder="Name...">
        </div>
        <div class="mb-3">
          <label for="img" class="form-label">Picture URL</label>
          <input v-model="accountData.picture" type="url" class="form-control" id="account" placeholder="url">
        </div>
        <div class="mb-3">
          <label for="img" class="form-label">Cover Image URL</label>
          <input v-model="accountData.coverImg" type="url" class="form-control" id="coverImg" placeholder="url">
        </div>
        <button class="btn btn-dark">Submit</button>
      </form>
    </div>
</template>

<script>
import { ref } from "vue";
import Pop from "../utils/Pop.js";
import { accountService } from "../services/AccountService.js";

export default {
setup() {
  const accountData = ref({})
  function resetForm() {
    accountData.value = { type: '' }
  }
  return {
    accountData,
    async editAccount(){
      try {
        await accountService.editAccount(accountData.value)
        resetForm()
      } catch (error) {
        Pop.error(error)
      }
    }
  };
},
};
</script>


<style>
</style>