<template>
  <section class="container">
    <div class="row">
      <div class="col-12">
        <div @click="setActiveKeep(keep?.id)">
          <button @click="deleteKeepFromVault(keep.id)" v-if="activeVault?.creatorId == user.id" class="btn btn-danger">
            <i class="mdi mdi-cancel"></i>
          </button>
          <img :src="keep?.img">
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-8">
        {{ keep?.name }}
      </div>

    </div>
  </section>
</template>

<script>
import { computed } from "vue";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { keepsService } from "../services/KeepsService.js";
import Pop from "../utils/Pop.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";

export default {
  props: { keep: { type: Object || Keep, required: true } },
  setup() {
    return {
      user: computed(() => AppState.user),
      activeVault: computed(()=> AppState.activeVault),
      async setActiveKeep(keepId) {
        try {
          keepsService.setActiveKeep(keepId)
        } catch (error) {
          Pop.error(error)
        }
      },
      // TODO: finish this once you can create a vaultKeep first.
      async deleteKeepFromVault(keepId){
        try {
          const activeVaultId = AppState.activeVault.id
          if(await Pop.confirm('Are you sure you want to delete this keep?', 'confirm')){
            await vaultKeepsService.deleteKeepFromVault(activeVaultId, keepId)
          }
        } catch (error) {
          Pop.error(error)
        }
      }
    };
  },
};
</script>


<style>
/* NOTE: had to pull this out of the masonry styling on HomeKeeps comp. Be wary. */
img {
  border-radius: 10px;
  width: 100%;
  margin-bottom: 1.25em;
}

.creator-img {
  border-radius: 50%;
  height: 5vh;
  object-fit: cover;
  object-position: center;
}
</style>