<template>
  <router-link :to="{ path: `vault/${vault?.id}` }">
    <section class="container background text-light elevation-2" :style="{ backgroundImage: vaultImg }" inline styling works>
      <div class="row justify-content-end button-adjust">
          <div @click="deleteVault.stop(vault?.id)" class="col-3 text-center button-background delete-button m-1" data-bs-dismiss="modal" v-if="vault?.creatorId == user.id">
            <i class="mdi mdi-cancel"></i>
          </div>
      </div>
      <section class="container keep-details" @click="getVaultById(vault?.id)">
        <div class="row">
          <div class="col-12 text-center mt-1 pb-2">
            <div class="name-background">
              {{ vault?.name }}
            </div>
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
    vaultImg: computed(() => `url(${props.vault.img})`),
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

.creator-img:hover{
  transform: scale(1.2);
  box-shadow: 5px 5px 5px 5px #383636;
}

.background {
  /* background-image: v-bind(vaultImg); */
  background-position: center;
  background-size: cover;
  border-radius: 10px;
  border: solid 3px #efefef77;
  transition: ease-in-out 0.3s;
  min-width: 25vh;
}

.background-img:hover{
  transform: scale(1.05);
}

.name-background{
  background-color: rgba(13, 12, 12, 0.621);
  padding: 0.3em;
  border-radius: 10px;
}

.delete-button{
  color: rgb(210, 22, 22);
  font-size: 25px;
}

.button-background:hover{
  transform: scale(1.2);
}

.button-adjust{
  padding-left: 1em;
}

.button-background{
  background-color: rgba(47, 44, 44, 0.715);
  width: 6.5vh;
  border-radius: 10px;
  transition: ease-in-out 0.5s;
}

/* NOTE: this creates the static height for the container, can't figure out how to make the background images work properly with the masonry container yet*** */
.keep-details{
  margin-top: 20vh;
  font-weight: 500;
}
</style>