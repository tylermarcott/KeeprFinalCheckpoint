<template>
  <div class="container-fluid">
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
          <!-- TODO: ok so this works in creating a vault keep. NOW, I just have to make sure that I am displaying the vaultKeeps in each vault that I look in, NOT just random keeps themselves. It needs to be a vault, then all of the vaultKeeps that have the vaultId. Call the corresponding keeps with vaultKeep.keepId, use getById to get each of these keeps and display them. -->
          <div class="col-2">
            <button @click="saveKeepToVault(activeKeep.id)" class="btn btn-dark">
              Save
            </button>
          </div>
          <div class="col-4">
            profile
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

export default {
  props: { keep: { type: Object || Keep, required: true } },
setup() {
  return {
    activeKeep: computed(() => AppState.activeKeep),
    async saveKeepToVault(keepId){
      try {
        const vaultId = AppState.activeVault.id
        const creatorId = AppState.user.id
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
</style>