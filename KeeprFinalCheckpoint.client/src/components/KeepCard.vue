<template>
  <section class="container background-img">
    <div class="row justify-content-end">
      <div class="col-2 text-end">
        <div @click="setActiveKeep(keep?.id)">
          <div class="delete-button" @click="deleteKeep(keep.id)" v-if="keep?.creatorId == user.id">
            <i class="mdi mdi-cancel"></i>
          </div>
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
      <div class="col-4">
      <router-link :to="{ path: `profile/${keep?.creatorId}` }"
        @click.stop.prevent="modal.getOrCreateInstance('#show-keep-details').hide()">
          <img :src="keep?.creator.picture">
        </router-link>
      </div>
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
import { logger } from "../utils/Logger.js";



export default {
  props: { keep: { type: Object || Keep, required: true } },
  setup(props) {
    const modal = Modal;
    return {
      modal,
      user: computed(() => AppState.user),
      cardImg: computed(() => `url(${props.keep.img})`),
      async setActiveKeep(keepId) {
        try {
          keepsService.setActiveKeep(keepId)
        } catch (error) {
          Pop.error(error)
        }
      },
      // FIXME: have to make this reactive and make sure the modal doesn't open when delete is clicked lol
      async deleteKeep(keepId) {
        try {
          if (await Pop.confirm('Are you sure you want to delete this keep?', 'confirm')) {
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
img {
  border-radius: 10px;
  width: 100%;
  margin-bottom: 1.25em;
}

.creator-img {
  border-radius: 50%;
  height: 5vh;
  object-fit: cover;
  object-position: center;
}

.background-img {
  background-image: v-bind(cardImg);
  background-position: center;
  object-fit: cover;
  border-radius: 10px;
  width: 100%;
  margin-bottom: 1.25em;
}

.delete-button{
  color: red;
}
</style>
