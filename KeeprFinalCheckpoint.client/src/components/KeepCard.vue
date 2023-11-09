<template>

<!-- NOTE: to fix modals, have to use a combination of .stop on the delete button, and modal getOrCreateInstance syntax on the actual modal I think? Need to fix this up, because it can be put on our portfolio. -->



  <section class="container background-img text-light elevation-2">
    <div class="row justify-content-end">
      <div class="col-2 text-end" data-bs-dismiss="modal">
        <div @click="setActiveKeep(keep?.id)">
          <div class="delete-button" @click="deleteKeep(keep.id)" v-if="keep?.creatorId == user.id">
            <i class="mdi mdi-cancel"></i>
          </div>
        </div>
      </div>
    </div>
    <div class="row keep-details">
      <div class="col-8 name-background">
        {{ keep?.name }}
      </div>
      <div class="col-4" data-bs-dismiss="modal">
      <router-link :to="{ path: `profile/${keep?.creatorId}` }">
          <img class="creator-img" :src="keep?.creator.picture">
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

  /* FIXME: created set height for ease of testing. Go back and try to fix masonry container later */
  min-height: 20vh;
  width: 100%;
}

.background-img:hover{
  transform: scale(1.05);
}

.name-background{
  background-color: rgba(40, 39, 39, 0.621);
  border-radius: 10px;
}

.delete-button{
  color: red;
  font-size: 20px;
}

.keep-details{
  margin-top: 20vh;
  font-weight: 800;
}
</style>
