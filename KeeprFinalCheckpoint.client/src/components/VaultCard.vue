<template>
  <router-link :to="{ path: `vault/${vault?.id}` }">
    <section class="container background text-light elevation-2">
      <div class="row justify-content-end">
        <div class="col-1">
          <button @click="deleteVault(vault?.id)" v-if="vault?.creatorId == user.id" class="btn btn-danger">
            <i class="mdi mdi-cancel"></i>
          </button>
        </div>
      </div>
      <section class="container" @click="getVaultById(vault?.id)">
        <div class="row">
        </div>
        <div class="row">
          <div class="col-8">
            {{ vault?.name }}   
          </div>
        </div>
      </section>
    </section>
  </router-link>
  </template>

<script>
import { computed } from "vue";
import { Vault } from "../models/Vault.js";
import { AppState } from "../AppState.js";
import {vaultsService} from '../services/VaultsService.js'
import Pop from "../utils/Pop.js";

export default {
  props: {vault: {type: Vault || Object, required: true}},
setup(props) {
  return {
    user: computed(()=> AppState.user),
    cardImg: computed(() => `url(${props.vault.img})`),
    async getVaultById(vaultId){
      try {
        await vaultsService.getVaultById(vaultId)
        this.getKeepsInVault(vaultId)
      } catch (error) {
        Pop.error(error)
      }
    },
      async getKeepsInVault(){
      try {
        const vaultId = AppState.activeVault.id
        await vaultsService.getKeepsInVault(vaultId)
      } catch (error) {
        Pop.error(error)
      }
    },
    async deleteVault(vaultId){
      try {
        if(await Pop.confirm('Are you sure you want to delete this vault?')){
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


<style>
.background {
  background-image: v-bind(cardImg);
  background-position: center;
  background-size: cover;
  border-radius: 10px;
  border: solid 3px #efefef77;
  transition: ease-in-out 0.3s;
  min-width: 25vh;
}
</style>