<template>
  <div class="container">
    <section>
      <div class="row justify-content-center">
        <div class="col-8 text-center">
          <!-- FIXME: fix this stretched image -->
          <img class="cover-img" :src="profile?.coverImg" alt="no-img">
        </div>
      </div>
      <div class="row justify-content-center">
        <div class="col-1">
          <img class="profile-img" :src="profile?.picture" :alt="profile?.name">
        </div>
        <div class="col-12 text-center headers">
          <h3>
            {{ profile?.name }}
          </h3>
        </div>
        <div class="col-12 text-center headers">
          {{ vaultSize }} Vaults | {{ keepSize }} Keeps
        </div>
      </div>
    </section>
    <section>
      <div class="row justify-content-center">
        <div class="col-8 headers">
          <h1>
            Vaults
          </h1>
          <div class="masonry-container">
            <div v-for="vault in vaults" :key="vault.id">
              <ProfileVaultCard :vault="vault"/>
            </div>
          </div>
        </div>
      </div>
    </section>
      <div class="row justify-content-center">
        <div class="col-8 headers">
          <h1>
            Keeps
          </h1>
          <div class="masonry-container">
            <div v-for="keep in keeps" :key="keep.id">
              <ModalWrapper id="show-keep-details">
                <template #button>
                  <div @click="getMyVaults">
                    <div @click="setActiveKeep(keep.id)">
                      <ProfileKeepCard :keep="keep"/>
                    </div>
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
    <section>

    </section>
  </div>
</template>

<script>
import { computed, onMounted } from "vue";
import Pop from "../utils/Pop.js";
import { profilesService } from "../services/ProfilesService.js";
import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { useRoute } from "vue-router";
import { keepsService } from "../services/KeepsService.js";
import { accountService } from "../services/AccountService.js";

// TODO: to fix this, I need to fix the router.js so the route.params can be used, I need the profile ID in the route. When the id is in the route, I can pull that route id, get my profile by id, then get the keeps and vaults by the profile. Everything needs to be done on this page, not on the keep card****

export default {
  setup() {
    const route = useRoute()
    onMounted(()=> {
      closeModal()
      getActiveProfile()
    })

    // TODO: split this into different functions, but it works for now
    async function getActiveProfile(){
      try {
        logger.log('getting the following profile id from route:', route.params.profileId)
        await profilesService.getProfileById(route.params.profileId)
        await profilesService.getProfileKeeps(route.params.profileId)
        await profilesService.getProfileVaults(route.params.profileId)
      } catch (error) {
        Pop.error(error)
      }
    }
    // FIXME: working prototype for closing modal lol
    function closeModal(){
      const detailsModal = document.getElementById('show-keep-details')
    }
  return {
    profile: computed(()=> AppState.activeProfile),
    keeps: computed(() => AppState.activeKeeps),
    vaults: computed(() => AppState.activeVaults),
    vaultSize: computed(()=> AppState.activeVaults.length),
    keepSize: computed(()=> AppState.activeKeeps.length),
    async getMyVaults(){
      await accountService.getMyVaults()
    },
    async setActiveKeep(keepId) {
      try {
        await keepsService.setActiveKeep(keepId)
      } catch (error) {
        Pop.error(error)
      }
    }
  };

},
};
</script>


<style>
.cover-img{
  height: 40vh;
  width: 40vh;
  min-height: 40vh;
  min-width: 40vh;
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

.headers{
  color: whitesmoke;
}
</style>