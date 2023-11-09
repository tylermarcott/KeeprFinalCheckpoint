<template>

<!-- NOTE: to fix modals, have to use a combination of .stop on the delete button, and modal getOrCreateInstance syntax on the actual modal I think? Need to fix this up, because it can be put on our portfolio. -->

  <section class="container background-img text-light elevation-2">
    <div class="row justify-content-end">
      <div @click="setActiveKeep(keep?.id)" class="col-3 text-center button-background delete-button m-1" data-bs-dismiss="modal" v-if="keep?.creatorId == user.id"> 
          <i class="mdi mdi-cancel"></i>
      </div>
    </div>
    <div class="row keep-details">
      <div class="col-8">
        <div class="name-background">
          {{ keep?.name }}
        </div>
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
          this.deleteKeep(keepId)
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
  border-radius: 10px;
  height: 5vh;
  object-fit: cover;
  object-position: center;
}

.background-img {
  background-image: v-bind(cardImg);
  background-position: center;
  background-size: cover;
  border-radius: 10px;
  transition: ease-in-out 0.3s;
}

.background-img:hover{
  transform: scale(1.05);
}

.name-background{
  background-color: rgba(40, 39, 39, 0.621);
  padding: 0.3em;
  border-radius: 10px;
}

.delete-button{
  color: rgb(210, 22, 22);
  font-size: 25px;
}

.button-background:hover{
  transform: scale(1.2);
}

.button-background{
  background-color: rgba(47, 44, 44, 0.715);
  width: 6.5vh;
  border-radius: 10px;
  transition: ease-in-out 0.5s;
}

.keep-details{
  margin-top: 20vh;
  font-weight: 500;
}
</style>
