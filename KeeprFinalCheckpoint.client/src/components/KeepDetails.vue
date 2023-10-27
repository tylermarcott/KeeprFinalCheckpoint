<template>
  <div class="container-fluid elevation-2">
    <!-- NOTE: Mick put this in and it crashed -->
    <!-- {{ keep.name }} {{ activeKeep.name }} -->
    <section class="row">
      <div class="col-6">
        <img :src="activeKeep?.img" :alt="activeKeep?.name">
      </div>
      <div class="col-6">
        <div class="row">
          <div class="col-2">
            <i class="mdi mdi-eye-outline"></i> {{ activeKeep?.views }}
          </div>
          <div class="col-2">
            <i class="mdi mdi-alpha-k-box-outline"></i> {{ activeKeep?.kept }}
          </div>
        </div>
        <div class="row">
          <h1>
            {{ activeKeep?.name }}
          </h1>
        </div>
        <div class="row">
          <p>
            {{ activeKeep?.description }}
          </p>
        </div>
        <div class="row">
          <div class="col-3">
              <VaultDropdown/>
          </div>
          <div class="col-2">
            <button @click="saveKeepToVault(activeKeep.id)" class="btn btn-dark">
              Save
            </button>
          </div>
          <div class="col-2" data-bs-dismiss="modal">
            <router-link :to="{ path: `profile/${keep?.creatorId}` }">
              <img class="creator-img" :src="activeKeep?.creator.picture">
            </router-link>
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<script>
import { computed } from "vue";
import { Keep } from "../models/Keep.js";
import { AppState } from "../AppState.js";
import Pop from "../utils/Pop.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import { logger } from "../utils/Logger.js";

export default {
  props: { keep: { type: Object || Keep, required: true } },
setup() {
  return {
    activeKeep: computed(() => AppState.activeKeep),
    async saveKeepToVault(keepId){
      try {
        const vaultId = AppState.activeVault.id
        const creatorId = AppState.user.id
        logger.log('double check on creatorId for save keep to vault:', creatorId)
        const vaultKeepData = {creatorId, vaultId, keepId}
        await vaultKeepsService.createVaultKeep(vaultKeepData)

      } catch (error) {
        Pop.error(error)
      }
    }
  };
},
};
</script>


<style>
.creator-img {
  border-radius: 40%;
  height: 5vh;
  object-fit: cover;
  object-position: center;
}
</style>