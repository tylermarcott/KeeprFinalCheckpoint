<template>

  <!-- FIXME: add syntax to be able t mark as priavte (checkbox) -->
  <div class="container form-class">
    <form @submit.prevent="createVault">
      <div class="mb-3">
        <label for="title" class="form-label">Vault Title</label>
        <input v-model="vaultData.name" type="title" class="form-control" id="keepTitle" placeholder="Title..."
          required="true">
      </div>
      <div class="mb-3">
        <label for="img" class="form-label">Vault Image URL</label>
        <input v-model="vaultData.img" type="url" class="form-control" id="keepImg" placeholder="url" required="true">
      </div>
      <div class="mb-3">
        <label for="description" class="form-label">Vault Description</label>
        <textarea v-model="vaultData.description" class="form-control" id="keepDescription" rows="3"></textarea>
      </div>
      <button class="btn btn-dark">Submit</button>
    </form>
  </div>
</template>

<script>
import { ref } from "vue";
import Pop from "../utils/Pop.js";
import { vaultsService } from "../services/VaultsService.js";

export default {
  setup() {
    const vaultData = ref({})
    // const router = useRouter()
    function resetForm() {
      vaultData.value = { type: '' }
    }
    return {
      vaultData,
      async createVault() {
        try {
          let newVault = await vaultsService.createVault(vaultData.value)
          resetForm()
          // FIXME: have to hide form once it is submitted.
          // NOTE: good example of hiding a modal, here.
          // Modal.getOrCreateInstance('#create-keep').hide()
          // FIXME: once you have details page created, reactivate this.
          // router.push({ name: 'Event Details', params: { eventId: newRecipe.id } })

        } catch (error) {
          Pop.error(error)
        }
      }
    };
  },
};
</script>


<style>
/* .form-class{
  z-index: 100;
} */
</style>