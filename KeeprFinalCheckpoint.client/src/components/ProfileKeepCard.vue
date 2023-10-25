<template>
  <section class="container">
    <div class="row">
      <div class="col-12">
        <div @click="setActiveKeep(keep?.id)">
          <button @click="deleteVaultKeep(keep.id)" v-if="activeVault?.creatorId == user.id" class="btn btn-danger">
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
import { logger } from "../utils/Logger.js";

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
      // NOTE: for this delete, see if you can use vue tools and see if the vaultKeepId is actually populating on the keeps that are in the specific vault. If they are, you can use this Id.
      // TODO: finish this once you can create a vaultKeep first.
      async deleteVaultKeep(keepId){
        try {
          const activeVaultId = AppState.activeVault.id
          logger.log('here is our vaultId:', activeVaultId)
          logger.log('here is our keepId:', keepId)
          logger.log('here is a list of our vaultKeeps:', AppState.vaultKeeps)
          const foundVaultKeep = AppState.vaultKeeps.find(vaultKeep => vaultKeep.keepId == keepId && vaultKeep.vaultId == activeVaultId)
          logger.log('found the following vaultKeep in Appstate:', foundVaultKeep)
          if(await Pop.confirm('Are you sure you want to delete this keep?', 'confirm')){
            await vaultKeepsService.deleteVaultKeep(foundVaultKeep.id)
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