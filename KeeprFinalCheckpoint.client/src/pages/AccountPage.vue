<template>
<div class="container">
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
    <!-- STUB: edit account -->
    <div>
      <div class="col-8">
        <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
          Edit Account <i class="mdi mdi-dots-horizontal"></i>
        </button>
        <!-- FIXME: ok, so display form works now, but the modal is on top of the form (everything is grayed out), and it won't go away. Need to fix this next. -->
        <ul class="dropdown-menu">
          <li>    
            <a class="dropdown-item" data-bs-target="#account-edit" data-bs-toggle="modal" href="#" @click.stop>
              Edit Account
            </a>
          </li>
        </ul>
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
            <ModalWrapper id="show-keep-details">
              <template #button>
                <KeepCard :keep="keep"/>
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
