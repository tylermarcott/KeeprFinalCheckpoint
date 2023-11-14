<template>
<div class="container">
  <section>
    <div class="row justify-content-center">
      <div class="col-8 text-center">
        <img class="cover-img" :src="account.coverImg" alt="">
      </div>
    </div>

    <div class="row justify-content-center">
      <div class="col-8 text-center">
        <img :src="account.picture" :alt="account.name">
      </div>
    </div>
    <!-- STUB: edit account -->
    <div class="row justify-content-center">
      <div class="col-8 text-center">
        <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
          <i title="Edit your account" class="mdi mdi-dots-horizontal"></i>
        </button>
        <ul class="dropdown-menu">
          <li>    
            <a class="dropdown-item" data-bs-target="#account-edit" data-bs-toggle="modal" href="#" @click.stop>
              Edit Account
            </a>
          </li>
        </ul>
      </div>
    </div>
    <div class="row justify-content-center">
      <div class="col-8 text-center my-2">
        <h3 class="headers mt-2">
          {{ account.name }}
        </h3>
      </div>
    </div>
  </section>
  
  <section class="container">
    <div class="row">
      <div class="col-8">
        <h1 class="headers mt-5">Vaults</h1>
      </div>
    </div>
    <div class="row justify-content-center">
      <div class="col-8">
        <div class="masonry-container">
          <div v-for="vault in vaults" :key="vault.id">
            <VaultCard :vault="vault"/>
          </div>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-8">
        <h1 class="headers mt-5">Keeps</h1>
      </div>
    </div>
    <div class="row justify-content-center mt-5">
      <div class="col-8">
        <div class="masonry-container">
          <div v-for="keep in keeps" :key="keep.id">
            <ModalWrapper id="show-keep-details">
              <template #button>
                <div @click="setActiveKeep(keep.id)">
                  <KeepCard :keep="keep"/>
                </div>
              </template>
              <template #body>
                <KeepDetails :keep="keep"/>
              </template>
            </ModalWrapper>
          </div>
        </div>
      </div>
    </div>
  </section>
</div>
</template>

<script>
import { computed, watchEffect } from 'vue';
import { AppState } from '../AppState';
import { accountService } from "../services/AccountService.js";
import { profilesService } from "../services/ProfilesService.js";
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js";
import { vaultsService } from "../services/VaultsService.js";
export default {
  setup() {
    watchEffect(()=> {
      getUserKeeps()
      getMyVaults()
    });
    async function getMyVaults(){
      await accountService.getMyVaults()
      const userId = AppState.user.id
      await profilesService.getProfileKeeps(userId)
    }
    async function getUserKeeps(){
      
    }
    return {
      account: computed(() => AppState.account),
      vaults: computed(()=> AppState.activeVaults),
      keeps: computed(()=> AppState.activeKeeps),
      async setActiveKeep(keepId){
        try {
          await keepsService.setActiveKeep(keepId)
        } catch (error) {
          Pop.error(error)
        }
      }
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}

.masonry-container{
  gap: 1.25em;
  columns: 200px;
  column-gap: 1.25em;
  img{
    border-radius: 10px;
    width: 100%;
    margin-bottom: 1.25em;
  }
}

.headers{
  color: whitesmoke;
}

.cover-img{
  min-height: 40vh;
  min-width: 40vh;
}
</style>
