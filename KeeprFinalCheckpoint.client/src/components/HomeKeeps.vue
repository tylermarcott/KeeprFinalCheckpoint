<template>
  <!-- FIXME: need to make the home page look better -->
  <div class="row">

    <div class="col-12 col-md-8">
      <div class="masonry-container">
        <div class="bg-dark text-light" v-for="keep in keeps" :key="keep.id">
          
          <ModalWrapper id="show-keep-details">
            <template #button>
              <KeepCard :keep="keep"/>
            </template>
        <template #body>
          <KeepDetails :keep="keep"/>
        </template>
      </ModalWrapper>
      
      
      
      <!-- <button v-if="keep.creatorId == user.id" class="btn btn-danger">
        <i class="mdi mdi-cancel"></i>
      </button>
      <img :src="keep.img">
      {{ keep.name }}
      {{ keep.creator.name }} -->
      
      
      
      
    </div>
    
    
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
    keeps: computed(()=> AppState.keeps),
    user: computed(()=> AppState.user)
   }
  }
};
</script>


<style lang="scss" scoped>
.masonry-container{
  gap: 1.25em;
  columns: 200px;
  column-gap: 1.25em;
  img{
    border-radius: 10px;
    width: 100%;
    margin-bottom: 1.25em;
  }
}
</style>