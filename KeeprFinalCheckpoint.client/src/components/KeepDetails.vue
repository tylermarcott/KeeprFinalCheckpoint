<template>
  <div class="container-fluid">
    <section class="row">
      <div class="col-6">
        <img :src="keep.img" :alt="keep.name">
      </div>
      <div class="col-6">
        <div class="row">
          <div class="col-2">
            <i class="mdi mdi-eye-outline"></i> {{ keep.views }}
          </div>
          <div class="col-2">
            <i class="mdi mdi-alpha-k-box-outline"></i> {{ keep.kept }}
          </div>
        </div>
        <div class="row">
          <h1>
            {{ keep.name }}
          </h1>
        </div>
        <div class="row">
          <p>
            {{ keep.description }}
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
import { Keep } from "../models/Keep.js";

export default {
  props: { keep: { type: Object || Keep, required: true } },
setup() {
  return {
  };
},
};
</script>


<style>
</style>