<template>
  <div class="about text-center">
    <h1>Welcome {{ account.name }}</h1>
    <img class="rounded-circle" :src="account.picture" alt="" />
    <p>{{ account.email }}</p>
  </div>

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
</template>

<script>
import { computed, watchEffect } from 'vue';
import { AppState } from '../AppState';
export default {
  setup() {
    watchEffect(()=> {
      getUserKeeps()
    });
    async function getUserKeeps(){
      // FIXME: need to add this next, I need to go into backend and make get my vaults first
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
