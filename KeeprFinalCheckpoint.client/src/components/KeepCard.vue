<template>
  <div @click="setActiveKeep(keep.id)">
    <button v-if="keep.creatorId == user.id" class="btn btn-danger">
      <i class="mdi mdi-cancel"></i>
    </button>
    <img :src="keep.img">
    {{ keep.name }}
    <!-- TODO: add creator img, get rid of name -->
    {{ keep.creator.name }}
  </div>
</template>

<script>
import { computed } from "vue";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { keepsService } from "../services/KeepsService.js";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";

export default {
  props: {keep: {type: Object || Keep, required: true}},
setup() {
  return {
    user: computed(()=> AppState.user),
    async setActiveKeep(keepId){
      try {
        keepsService.setActiveKeep(keepId)
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
  img{
    border-radius: 10px;
    width: 100%;
    margin-bottom: 1.25em;
  }
</style>