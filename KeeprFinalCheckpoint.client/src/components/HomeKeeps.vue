<template>
  <!-- FIXME: need to make the home page look better -->
  <div class="row justify-content-center">

    <div class="col-12 col-md-8">
      <div class="masonry-container">
        <div class="text-light mb-3" v-for="keep in keeps" :key="keep.id">
          <ModalWrapper id="show-keep-details">
            <template #button>
              <div @click="getMyVaults">
                <KeepCard :keep="keep"/>
              </div>
            </template>
        <template #body>
          <KeepDetails :keep="keep"/>
        </template>
      </ModalWrapper>
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
import { accountService } from "../services/AccountService.js";
import { Keep } from "../models/Keep.js";
export default {
  // FIXME: need to try and figure out how I can set the backgrounds of the keep cards to the keep img. Couldn't get it from the artTerminal example....
  // props: {keep: {type: Keep, required: true}},
  setup(props){
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
    user: computed(()=> AppState.user),
    // cardImg: computed(()=> `url(${props.keep.img})`),
      async getMyVaults() {
        try {
          await accountService.getMyVaults()
        } catch (error) {
          Pop.error(error)
        }
      }
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

// .background-img{
//   background-image: v-bind(cardImg);
// }
</style>