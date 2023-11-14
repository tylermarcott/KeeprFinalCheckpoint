<template>
  <header>
    <!-- FIXME: change this back to 'navbar' along with import and comp below when you are done testing -->
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <footer>
  </footer>

  <ModalWrapper :id="'keep-create'" class="modal-background">
    <template #body>
      <KeepForm/>
    </template>
  </ModalWrapper>

  <ModalWrapper :id="'account-edit'" class="modal-background">
      <template #body>
        <AccountForm/>
      </template>
    </ModalWrapper>

  <ModalWrapper :id="'vault-create'" class="modal-background">
    <template #body>
      <VaultForm/>
    </template>
  </ModalWrapper>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root{
  --main-height: calc(100vh - 32px - 64px);
}

header{
    background-color: rgb(31, 30, 30);

}

main{
    background-color: rgb(31, 30, 30);
    padding-bottom: 4em;
  }

footer{
  background-color: rgb(31, 30, 30);
}

.modal-background{
  background-color: rgb(31, 30, 30);
}
</style>
