<template>
  <div class="container">
    <section>
      <div class="row justify-content-center">
        <img class="vault-cover-img" :src="vault?.img" alt="">
      </div>
      <div class="row headers text-center">
        <h4>
          {{ vault?.name }} 
        </h4>
        <p class="fs-5">by</p>
        <h4>
          {{ vault?.creator.name }}
        </h4>
      </div>
      <div class="row headers text-center mb-5">
        <h5>
          {{ keepLength }} Keeps
        </h5>
      </div>
    </section>
    <section>
      
      <div class="col-12 col-md-7 col-lg-9">
        <div class="masonry-container">
          <div class="elevation-2 rounded text-light mb-2" v-for="keep in keeps" :key="keep.id">

            <!-- FIXME: I need the ability to be able to delete any keep out of the vualt, without deleting the keep itself. But I need them to look like the normal keep cards. Maybe just copy the keep card onto profile keep card -->
            
            <ModalWrapper id="show-keep-details">
              <template #button>
              <ProfileKeepCard :keep="keep"/>
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
import { computed, onMounted } from "vue";
import { AppState } from "../AppState.js";
import { useRoute } from "vue-router";
import Pop from "../utils/Pop.js";
import { vaultsService } from "../services/VaultsService.js";
import { logger } from "../utils/Logger.js";
import { router } from "../router.js";


export default {
setup() {
  const route = useRoute()
  onMounted(()=> {
    getVaultById()
  })
  async function getVaultById(){
    try {
      const vaultId = route.params.vaultId
      await vaultsService.getVaultById(vaultId)
      getKeepsInVault(vaultId)
    } catch (error) {
      router.push({ name: 'Home' })
      logger.error(error)
      Pop.toast('You do not have access to this!', 'error', 'center')
    }
  }
    async function getKeepsInVault(vaultId){
      try {
        await vaultsService.getKeepsInVault(vaultId)
      } catch (error) {
        Pop.error(error)
      }
    }
  return {
    vault: computed(()=> AppState.activeVault),
    keeps: computed(()=> AppState.activeKeeps),
    user: computed(()=> AppState.user),
    keepLength: computed(()=> AppState.activeKeeps.length)
  };
},
};
</script>


<style>
.vault-cover-img{
  object-fit: cover;
  object-position: center;
  height: 35vh;
  width: 60vh;
  margin: 1em;
  padding: 0vw;
  border-radius: 5px !important;
}

.headers{
  color: whitesmoke;
}

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