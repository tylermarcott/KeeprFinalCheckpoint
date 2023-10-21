<template>
  <nav class="navbar navbar-expand-lg navbar-dark px-3">
    <router-link class="navbar-brand d-flex text-dark" :to="{ name: 'Home' }">
      Home
    </router-link>
    <div class="me-4">
        <div class="dropdown">
          <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
            create
          </button>
          <ul class="dropdown-menu">
            <li><a class="dropdown-item" href="#">
              <ModalWrapper id="create-keep" v-if="user.isAuthenticated">
                <template #button>
                  <button class="btn btn-light">
                    new keep
                  </button>
                </template>
                <template #body>
                  <KeepForm/>
                </template>
              </ModalWrapper>
            </a></li>
            <li><a class="dropdown-item" href="#">new vault

            </a></li>
          </ul>
        </div>
    </div>
    <h3>the keepr co.</h3>

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
import ModalWrapper from '../components/ModalWrapper.vue'
export default {
  setup() {
    return {
      user: computed(()=> AppState.user)
    }
  },
  components: { Login, ModalWrapper }
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
</style>
