<template>
<div class="container">


<!-- TODO: work on this next -->


  <section>
    <div class="row">
      <div class="col-8">
        account cover image here
      </div>
    </div>

    <div class="row">
      <div class="col-8">
        <img :src="account.picture" :alt="account.name">
      </div>
    </div>
    <div class="row">
      <div class="col-8">
        <h3>
          {{ account.name }}
        </h3>
      </div>
    </div>
  </section>
  
  <section class="container">
    <div class="row">
      <div class="col-8">
        <h1>Vaults</h1>
      </div>
    </div>
    <div class="row">
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
        <h1>Keeps</h1>
      </div>
    </div>
    <div class="row">
      <div class="col-8">
        <div class="masonry-container">
          <div v-for="keep in keeps" :key="keep.id">
            <ProfileKeepCard :keep="keep"/>
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
      keeps: computed(()=> AppState.activeKeeps)
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
</style>
