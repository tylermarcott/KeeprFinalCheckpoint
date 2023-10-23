<template>
  hello from profile page
</template>
<!-- TODO: onclick, active profile is set. Now I just need to compute and populate keeps and vaults -->

<script>
import { onMounted } from "vue";
import Pop from "../utils/Pop.js";
import { profilesService } from "../services/ProfilesService.js";
import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";

export default {
setup() {
  onMounted(()=>{
    
    // getProfileKeeps(),
    getProfileVaults()
  });
  // TODO: have to get the user's profile first before I can use their profile to get their vaults and keeps.
  // ANCHOR: direction: get vaults and keeps to profile page, go to backend and finish vaults by account, then fill out account page
  // async function getProfileKeeps(){
  //   try {
  //     const profileId = AppState.activeProfile.id
  //     await profilesService.getProfileKeeps(profileId)
  //   } catch (error) {
  //     Pop.error(error)
  //   }
  // }
  async function getProfileVaults(){
    const profileId = AppState.activeProfile.id
    logger.log('here is the profile id of the active profile:', profileId)
    await profilesService.getProfileVaults(profileId)
  }
  return {};
},
};
</script>


<style>
</style>