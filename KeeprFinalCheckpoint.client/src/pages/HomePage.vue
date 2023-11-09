<template>
  <div class="container-fluid">
    <section class="row justify-content-center">
      <div class="col-12 col-md-8">
        <div class="masonry-container">
          <div class="text-light mb-3 text-center" v-for="keep in keeps" :key="keep.id">
            <ModalWrapper id="show-keep-details">
              <template #button>
                <div @click="getMyVaults(keep.id)">
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
    </section>
  </div>
</template>

<script>
import { AppState } from '../AppState';
import { computed, watchEffect } from 'vue';
import { keepsService } from '../services/KeepsService.js'
import Pop from "../utils/Pop.js";
import { accountService } from "../services/AccountService.js";
export default {
  setup() {
    watchEffect(() => {
      getKeeps();
    });

    async function getKeeps() {
      try {
        await keepsService.getKeeps();
      } catch (error) {
        Pop.error(error)
      }
    }
    return {
      keeps: computed(() => AppState.keeps),
      user: computed(() => AppState.user),
      async getMyVaults(keepId) {
        try {
          await keepsService.setActiveKeep(keepId)
          await accountService.getMyVaults()
        } catch (error) {
          Pop.error(error)
        }
      }
    }
  }
};
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;
}

.masonry-container{
  $gap: 1em;
  columns: 200px;
  column-gap: $gap;
  img{
    border-radius: 10px;
    width: 100%;
    margin-bottom: $gap;
  }
}
</style>
