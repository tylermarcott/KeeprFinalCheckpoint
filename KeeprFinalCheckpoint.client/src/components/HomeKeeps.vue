<template>
  <div class="col-12 col-md-7 col-lg-9">
    <div class="masonry-container">
      <div class="bg-dark text-light" v-for="keep in keeps" :key="keep.id">
        <button v-if="keep.creatorId == user.id" class="btn btn-danger">
          <i class="mdi mdi-cancel"></i>
        </button>
        <img :src="keep.img">
        {{ keep.name }}
      </div>
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
    keeps: computed(()=> AppState.activeKeeps),
    user: computed(()=> AppState.user)
   }
  }
};
</script>


<style lang="scss" scoped>
.masonry-container{
  $gap: 1.25em;
  columns: 200px;
  column-gap: $gap;
  img{
    border-radius: 10px;
    width: 100%;
    margin-bottom: $gap;
  }
}
</style>