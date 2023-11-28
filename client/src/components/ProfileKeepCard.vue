<template>
  <!-- NOTE: to fix modals, have to use a combination of .stop on the delete button, and modal getOrCreateInstance syntax on the actual modal I think? Need to fix this up, because it can be put on our portfolio. -->

  <section class="container background-img text-light elevation-2" :style="{ backgroundImage: keepImg }">
    <div class="row justify-content-end">
      <div @click="deleteVaultKeep(keep.vaultKeepId)" v-if="activeVault?.creatorId == user.id" class="col-3 text-center button-background delete-button m-1" >
        <i class="mdi mdi-cancel"></i>
      </div>
    </div>
    <div class="row keep-details">
      <div v-if="keep.creatorId != user.id" class="col-8 mt-1 pb-2">
        <div class="name-background">
          {{ keep?.name }}
        </div>
      </div>
      <div v-else class="col-12 text-center mt-1 pb-2">
        <div class="name-background">
          {{ keep?.name }}
        </div>
      </div>
      <div v-if="keep.creatorId != user.id" class="col-4" data-bs-dismiss="modal">
        <router-link :to="{ path: `profile/${keep?.creatorId}` }">
          <img class="creator-img" :src="keep?.creator.picture">
        </router-link>
      </div>
      <div v-else class="pb-3">
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
import { vaultsService } from "../services/VaultsService.js";

export default {
  props: { keep: { type: Object || Keep, required: true } },
  setup(props) {
    return {
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      activeVault: computed(()=> AppState.activeVault),
      keepImg: computed(() => `url(${props.keep.img})`),
      async setActiveKeep(keepId) {
        try {
          keepsService.setActiveKeep(keepId)
        } catch (error) {
          Pop.error(error)
        }
      },
      async deleteVaultKeep(vaultKeepId){
        try {
          logger.log('here are our vaultKeeps:', AppState.vaultKeeps)

          if(await Pop.confirm('Are you sure you want to delete this keep?', 'confirm')){
            await vaultKeepsService.deleteVaultKeep(vaultKeepId)
          }
        } catch (error) {
          Pop.error(error)
        }
      },
      async getKeepsInVault(vaultId){
      try {
        await vaultsService.getKeepsInVault(vaultId)
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
.creator-img {
  border-radius: 10px !important;
  height: 6vh !important;
  aspect-ratio: 1/1;
  object-fit: cover !important;
  object-position: center !important;
  transition: ease-in-out 0.5s;
}

.creator-img:hover{
  transform: scale(1.2);
  box-shadow: 5px 5px 5px 5px #383636;
}

.background-img {
  background-image: v-bind(cardImg);
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

.button-background{
  background-color: rgba(47, 44, 44, 0.715);
  width: 6.5vh;
  border-radius: 10px;
  transition: ease-in-out 0.5s;
}

.keep-details{
  margin-top: 20vh;
  font-weight: 500;
}
</style>