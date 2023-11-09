<template>
  <nav class="navbar navbar-expand-lg navbar-dark px-3 mb-5 mt-4">
    <router-link class="navbar-brand d-flex text-dark" :to="{ name: 'Home' }">
      <div title="Go back to the home page." class="home-button elevation-2">
        Home
      </div>
    </router-link>
    <div class="me-4" v-if="user.isAuthenticated">
        <div class="dropdown">
          <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
            create
          </button>
          <!-- FIXME: ok, so display form works now, but the modal is on top of the form (everything is grayed out), and it won't go away. Need to fix this next. -->
          <ul class="dropdown-menu">
            <li>    
              <a class="dropdown-item" data-bs-target="#keep-create" data-bs-toggle="modal" href="#" @click.stop>
                new keep
              </a>
            </li>
            <li>
              <a class="dropdown-item" data-bs-target="#vault-create" data-bs-toggle="modal" href="#" @click.stop>
                new vault
              </a>
            </li>
          </ul>
        </div>
    </div>
    <div class="background-card elevation-2">
      <h3 class="title">the keepr co.</h3>
    </div>
    

    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <!-- <li>
          <router-link :to="{ name: 'Home' }" class="btn text-success lighten-30 selectable text-uppercase">
            Home
          </router-link>
        </li> -->
        <li>
          <!-- Create -->
        </li>
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
</template>

<script>
import { computed } from "vue";
import Login from './Login.vue';
import { AppState } from "../AppState.js";
export default {
  setup() {
    return {
      user: computed(()=> AppState.user)
    }
  },
  components: { Login}
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
.title{
  font-family: 'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;
  font-size: 40px;
  padding: 0.3em;
}

.background-card{
  border: solid 3px #acacacbd;
  background-color: whitesmoke;
  border-radius: 10px;
  margin-left: 50vh;
}

.home-button{
  font-family: 'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;
  font-size: 30px;
  transition: ease-in-out 0.5s;
  background-color: whitesmoke;
  border: solid 3px #acacacbd;
  border-radius: 10px;
  padding: 0.2em;
}

.home-button:hover{
  transform: scale(1.1);
}
</style>
