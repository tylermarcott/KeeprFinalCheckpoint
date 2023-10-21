<template>
  <div class="container">
    <form @submit="createKeep">
      <div class="mb-3">
        <label for="title" class="form-label">Keep Title</label>
        <input v-model="keepData.title" type="title" class="form-control" id="keepTitle"
          placeholder="Title..." required="true">
      </div>
      <div class="mb-3">
        <label for="img" class="form-label">Keep Image URL</label>
        <input v-model="keepData.img" type="url" class="form-control" id="keepImg" placeholder="url" required="true">
      </div>
      <!-- <div class="mb-3">
        <label for="tags" class="form-label">Keep Tags</label>
        <input v-model="keepData.tag" class="form-control" id="keepTag" placeholder="tag">
      </div> -->
        <div class="mb-3">
          <label for="description" class="form-label">Keep Description</label>
          <textarea v-model="keepData.description" class="form-control" id="keepDescription" rows="3"></textarea>
        </div>
      <button class="btn btn-dark">Submit</button>
    </form>
  </div>
</template>

<script>
import { ref } from "vue";
import Pop from "../utils/Pop.js";
import { useRouter } from "vue-router";
import { Modal } from "bootstrap";
import { keepsService } from "../services/KeepsService.js";

export default {
  setup() {
    const keepData = ref({})
    // const router = useRouter()
    function resetForm() {
      keepData.value = { type: '' }
    }
    return {
      keepData,
      async createKeep() {
        try {
          let newKeep = await keepsService.createKeep(keepData.value)
          resetForm()
          Modal.getOrCreateInstance('#create-keep').hide()
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


<style></style>