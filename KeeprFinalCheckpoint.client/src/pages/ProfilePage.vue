<template>
  <div class="container">
    <div class="row">
      <div class="col-8">
        put user profile cover img here
      </div>
    </div>
    <div class="row">
      <div class="col-1">
        <img class="profile-img" :src="profile.picture" :alt="profile.name">
      </div>
      <div class="col-12">
        <h3>
          {{ profile.name }}
        </h3>
      </div>
      <div class="col-12">
        vault number | keep number
      </div>
    </div>
    <div class="row">
      <div class="col-8">
        <h1>
          Vaults
        </h1>
        <div  class="masonry-container">
          <div v-for="vault in vaults" :key="vault.id">
            <VaultCard :vault="vault"/>
          </div>
        </div>
      </div>
    </div>
  </div>



</template>
<!-- TODO: onclick, active profile is set. Now I just need to compute and populate keeps and vaults -->

<script>
import { computed, onMounted, watchEffect } from "vue";
import Pop from "../utils/Pop.js";
import { profilesService } from "../services/ProfilesService.js";
import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";

// TODO: to fix this, I need to fix the router.js so the route.params can be used, I need the profile ID in the route. When the id is in the route, I can pull that route id, get my profile by id, then get the keeps and vaults by the profile. Everything needs to be done on this page, not on the keep card****

export default {
  setup() {
    // watchEffect(()=> {
    //   getActiveProfile(),
    //   getProfileKeeps(),
    //   getProfileVaults()
    // })

    // function getActiveProfile(){
    //   try {
    //     const activeProfile = profilesService.getActiveProfile()
    //     return activeProfile
    //   } catch (error) {
    //     Pop.error(error)
    //   }
    // }
    // async function getProfileKeeps() {
    //   try {
    //     const profileId = this.getActiveProfile.id
    //     await profilesService.getProfileKeeps(profileId)
    //   } catch (error) {
    //     Pop.error(error)
    //   }
    // }

    // async function getProfileVaults() {
    //   try {
    //     const profileId = this.getActiveProfile.id
    //     await profilesService.getProfileVaults(profileId)
    //   } catch (error) {
    //     Pop.error(error)
    //   }
    // }
  return {
    profile: computed(()=> AppState.activeProfile),
    keeps: computed(() => AppState.activeKeeps),
    vaults: computed(() => AppState.activeVaults),
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