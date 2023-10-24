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
        get vaults by account here
      </div>
    </div>
    <div class="row">
      <div class="col-8">
        <h1>Keeps</h1>
      </div>
    </div>
    <div class="row">
      <div class="col-8">
        get keeps by account here
      </div>
    </div>
  </section>
</div>
</template>

<script>
import { computed, watchEffect } from 'vue';
import { AppState } from '../AppState';
import { accountService } from "../services/AccountService.js";
export default {
  setup() {
    watchEffect(()=> {
      getUserKeeps()
      getMyVaults()
    });
    async function getMyVaults(){
      await accountService.getMyVaults()
    }
    async function getUserKeeps(){
      
    }
    return {
      account: computed(() => AppState.account)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
