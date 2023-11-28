<template>
  <!-- NOTE: we want this to create a vaultKeep relationship between the keep we are on, and the vault that is selected -->
  <div class="d-flex w-100">
    <!-- FIXME: make this list scrollable. -->
      <h5 type="button" data-bs-toggle="dropdown">{{ activeVault ? activeVault.name : "Select Vault" }}</h5>
    <ul class="dropdown-menu col-4 col-md-3">
      <li v-for="vault in vaults" :key="vault.id">
          <h5 @click="setActiveVault(vault.id)" class="event-dropdown selectable text-center">{{ vault.name }}</h5>
      </li>
    </ul>
  </div>
</template>

<script>
import { computed, onMounted } from "vue";
import { AppState } from "../AppState.js";
import { vaultsService } from "../services/VaultsService.js";
import { accountService } from "../services/AccountService.js";

export default {
setup() {
  return {
    vaults: computed(()=> AppState.activeVaults),
    activeVault: computed(()=> AppState.activeVault),
    setActiveVault(vaultId){
      vaultsService.setActiveVault(vaultId)
    }
  };
},
};
</script>


<style>
</style>