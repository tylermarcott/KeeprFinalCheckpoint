<template>
  <div class="col-6 col-md-3">
    <div class="masonry-container">
      <img src="" alt="">
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watchEffect } from 'vue';
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

   }
  }
};
</script>


<style lang="scss" scoped>

</style>