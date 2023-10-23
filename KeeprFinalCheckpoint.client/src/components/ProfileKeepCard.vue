<template>
  <section class="container">
    <div class="row">
      <div class="col-12">
        <div @click="setActiveKeep(keep?.id)">
          <button v-if="keep?.creatorId == user.id" class="btn btn-danger">
            <i class="mdi mdi-cancel"></i>
          </button>
          <img :src="keep?.img">
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-8">
        {{ keep?.name }}
      </div>
      <!-- FIXME: need to prevent the modal from opening when clicking on the user img for router link -->

    </div>
  </section>
</template>

<script>
import { computed } from "vue";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";

export default {
  props: { keep: { type: Object || Keep, required: true } },
  setup() {
    return {
      user: computed(() => AppState.user)
    };
  },
};
</script>


<style>
/* NOTE: had to pull this out of the masonry styling on HomeKeeps comp. Be wary. */
img {
  border-radius: 10px;
  width: 100%;
  margin-bottom: 1.25em;
}

.creator-img {
  border-radius: 50%;
  height: 5vh;
  object-fit: cover;
  object-position: center;
}
</style>