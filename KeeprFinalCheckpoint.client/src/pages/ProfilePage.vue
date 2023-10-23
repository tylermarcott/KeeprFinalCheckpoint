<template>
  hello from profile page
</template>
<!-- TODO: onclick, active profile is set. Now I just need to compute and populate keeps and vaults -->

<script>
import { onMounted } from "vue";

export default {
setup() {
  onMounted(()=>{
    getProfileKeeps(),
    getProfileVaults()
  });
  // TODO: have to get the user's profile first before I can use their profile to get their vaults and keeps.
  async function getProfileKeeps(){

  }
  async function getProfileVaults(){

  }
  return {};
},
};
</script>


<style>
</style>