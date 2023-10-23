<template>
  <div class="container-fluid">
    <section class="row">
      <div class="col-6">
        <img :src="activeKeep?.img" :alt="activeKeep?.name">
      </div>
      <div class="col-6">
        <div class="row">
          <div class="col-2">
            <i class="mdi mdi-eye-outline"></i> {{ activeKeep?.views }}
          </div>
          <div class="col-2">
            <i class="mdi mdi-alpha-k-box-outline"></i> {{ activeKeep?.kept }}
          </div>
        </div>
        <div class="row">
          <h1>
            {{ activeKeep?.name }}
          </h1>
        </div>
        <div class="row">
          <p>
            {{ activeKeep?.description }}
          </p>
        </div>
        <div class="row">
          <div class="col-3">
            dropdown
          </div>
          <div class="col-2">
            save button
          </div>
          <div class="col-4">
            profile
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<script>
import { computed } from "vue";
import { Keep } from "../models/Keep.js";
import { AppState } from "../AppState.js";

export default {
  props: { keep: { type: Object || Keep, required: true } },
setup() {
  return {
    activeKeep: computed(() => AppState.activeKeep)
  };
},
};
</script>


<style>
</style>