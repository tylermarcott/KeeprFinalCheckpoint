<template>
  <section class="elevation-2">
    <router-link :to="{ path: `/vault/${vault?.id}` }">
      <section class="container">
        <div class="row">
          <div class="col-12">
            <div @click="getVaultById(vault?.id)">
              <img :src="vault?.img">
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-8">
            {{ vault?.name }}
          </div>
        </div>
      </section>
    </router-link>
  </section>
</template>

<script>
import { computed } from "vue";
import { Vault } from "../models/Vault.js";
import { AppState } from "../AppState.js";
import { vaultsService } from '../services/VaultsService.js'
import Pop from "../utils/Pop.js";

export default {
  props: { vault: { type: Vault || Object, required: true } },
  setup() {
    return {
      user: computed(() => AppState.user),
      async getVaultById(vaultId) {
        try {
          await vaultsService.getVaultById(vaultId)
          this.getKeepsInVault(vaultId)
        } catch (error) {
          Pop.error(error)
        }
      },
      async getKeepsInVault() {
        try {
          const vaultId = AppState.activeVault.id
          await vaultsService.getKeepsInVault(vaultId)
        } catch (error) {
          Pop.error(error)
        }
      },
      // FIXME: this deletes, but I get errors because it still pushes to the vault page on-click
      async deleteVault(vaultId) {
        try {
          if (await Pop.confirm('Are you sure you want to delete this vault?')) {
            await vaultsService.deleteVault(vaultId)
          }
        } catch (error) {
          Pop.error(error)
        }
      }
    };
  },
};
</script>


<style></style>