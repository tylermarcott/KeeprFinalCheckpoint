<template>
  VAULT PAGE
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
    </section>
  </div>
</template>

<script>
import { computed} from "vue";
import { AppState } from "../AppState.js";


export default {
setup() {
  return {
    vault: computed(()=> AppState.activeVault)
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
</style>