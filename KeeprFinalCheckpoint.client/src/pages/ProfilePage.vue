<template>
  <div class="container">
    <section>
      <div class="row">
        <div class="col-8">
          put user profile cover img here
        </div>
      </div>
      <div class="row">
        <div class="col-1">
          <img class="profile-img" :src="profile?.picture" :alt="profile?.name">
        </div>
        <div class="col-12">
          <h3>
            {{ profile?.name }}
          </h3>
        </div>
        <div class="col-12">
          vault number | keep number
        </div>
      </div>
    </section>
    <section>
      <div class="row">
        <div class="col-8">
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
      <div class="row">
        <div class="col-8">
          <h1>
            Keeps
          </h1>
        </div>
        <div class="masonry-container">
          <div v-for="keep in keeps" :key="keep.id">
            <ModalWrapper id="show-keep-details">
                <template #button>
                  <div @click="setActiveKeep(keep.id)">
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
.profile-img{
  border-radius: 50%;
  height: 6vh;
  aspect-ratio: 1/1;
  object-position: cover;
  object-fit: center;
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