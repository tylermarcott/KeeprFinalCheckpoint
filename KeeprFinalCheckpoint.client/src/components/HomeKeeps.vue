<template>
  <div class="col-6 col-md-3">
    <div class="masonry-container">
      <img class="keep-img" v-for="keep in keeps" :key="keep.id" :src="keep.img" :alt="keep.name">
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, watchEffect } from 'vue';
import {keepsService} from '../services/KeepsService.js'
import Pop from "../utils/Pop.js";
export default {
  setup(){
    watchEffect(() => {
      getKeeps();
    });

    async function getKeeps(){
      try {
        await keepsService.getKeeps();
      } catch (error) {
        Pop.error(error)
      }
    }
  return { 
    keeps: computed(()=> AppState.activeKeeps)
   }
  }
};
</script>


<style lang="scss" scoped>
.keep-img{
  max-height: 30vh;
  aspect-ratio: 1/1;
  object-fit: cover;
  object-position: center;
}
</style>