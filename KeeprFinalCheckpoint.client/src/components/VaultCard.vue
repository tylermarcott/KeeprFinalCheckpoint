<template>
  <router-link :to="{ name: 'Vault' }">
    <section class="container">
        <div class="row">
          <div class="col-12">
            <div @click="setActiveVault(vault.id)">
              <button v-if="vault.creatorId == user.id" class="btn btn-danger">
                <i class="mdi mdi-cancel"></i>
              </button>
              <img :src="vault.img">
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-8">
            {{ vault.name }}   
          </div>
        </div>
      </section>
    </router-link>
  </template>

<script>
import { Vault } from "../models/Vault.js";

export default {
  props: {vault: {type: Vault || Object, required: true}},
setup() {
  return {};
},
};
</script>


<style>
</style>