<template>
  <div class="container">
    <section>
      <div class="row justify-content-center">
        <img class="vault-cover-img" :src="vault?.img" alt="">
      </div>
      <div class="row text-center">
        <h4>
          {{ vault?.name }} by {{ vault?.creator.name }}
        </h4>
      </div>
      <div class="row text-center mb-5">
        <h5>
          {{ keepLength }} Keeps
        </h5>
      </div>
    </section>
    <section>
      
      <div class="col-12 col-md-7 col-lg-9">
        <div class="masonry-container">
          <div class="bg-dark text-light" v-for="keep in keeps" :key="keep.id">
            
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
import { computed} from "vue";
import { AppState } from "../AppState.js";


export default {
setup() {
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
  height: 20vh;
  width: 50vh;
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