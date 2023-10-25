<template>
  <!-- FIXME: having issue with identifying form stuff lol -->
  <div class="container form-class">
      <form @submit.prevent="editAccount">
        <div class="mb-3">
          <label for="name" class="form-label">Account Name</label>
          <input v-model="accountData.name" type="name" class="form-control" id="accountName"
            placeholder="Name..." required="true">
        </div>
        <div class="mb-3">
          <label for="img" class="form-label">Picture URL</label>
          <input v-model="accountData.img" type="url" class="form-control" id="account" placeholder="url" required="true">
        </div>
        <div class="mb-3">
          <label for="img" class="form-label">Cover Image URL</label>
          <input v-model="accountData.coverImg" type="url" class="form-control" id="coverImg" placeholder="url" required="true">
        </div>
        <button class="btn btn-dark">Submit</button>
      </form>
    </div>
</template>

<script>
import { ref } from "vue";

export default {
setup() {
  const accountData = ref({})
  return {
    accountData
  };
},
};
</script>


<style>
</style>