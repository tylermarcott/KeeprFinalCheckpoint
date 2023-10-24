<template>
  <section class="container">
    <div class="row">
      <div class="col-12">
        <div @click="setActiveKeep(keep?.id)">
          <button @click="deleteKeep(keep.id)" v-if="keep?.creatorId == user.id" class="btn btn-danger">
            <i class="mdi mdi-cancel"></i>
          </button>
          <img :src="keep?.img">
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-8">
        {{ keep?.name }}   
      </div>
      <!-- FIXME: see ProjectCard on ArtTerminal to see reference of how to set the background image of the card to the keep or vault image. -->
      <!-- FIXME: need to prevent the modal from opening when clicking on the user img for router link -->
      <!-- NOTE: put this router link into keep details modal!!!! -->
      <!-- NOTE: Jake says 'move your modal wrapper down man!''  -->
      <router-link :to="{ path: `profile/${keep?.creatorId}` }" @click.stop.prevent="modal.getOrCreateInstance('#show-keep-details').hide()">
        <div class="col-4">
            <img :src="keep?.creator.picture">
        </div>
      </router-link>
    </div>
  </section>
</template>

<script>
import { computed } from "vue";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js";
import { Modal } from "bootstrap";



export default {
  props: {keep: {type: Object || Keep, required: true}},
setup() {
  const modal = Modal;
  return {
    modal,
    user: computed(()=> AppState.user),
    async setActiveKeep(keepId){
      try {
        keepsService.setActiveKeep(keepId)
        // Modal.getOrCreateInstance('#show-keep-details').hide()
      } catch (error) {
        Pop.error(error)
      }
    },
    // FIXME: have to make this reactive and make sure the modal doesn't open when delete is clicked lol
    async deleteKeep(keepId){
      try {
        // FIXME: have to make this right, it doesn't wait for confirmation to delete as it stands.
        if(Pop.confirm('Are you sure you want to delete this keep?', 'confirm')){
          await keepsService.deleteKeep(keepId)
        }
      } catch (error) {
        Pop.error(error)
      }
    }
  };
},
};
</script>


<style>
/* NOTE: had to pull this out of the masonry styling on HomeKeeps comp. Be wary. */
  img{
    border-radius: 10px;
    width: 100%;
    margin-bottom: 1.25em;
  }

  .creator-img{
    border-radius: 50%;
    height: 5vh;
    object-fit: cover;
    object-position: center;
  }
</style>