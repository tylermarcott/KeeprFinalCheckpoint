<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
   <footer>
  </footer>

  <ModalWrapper :id="'keep-create'">
    <template #body>
      <KeepForm/>
    </template>
  </ModalWrapper>

  <ModalWrapper :id="'account-edit'">
      <template #body>
        <AccountForm/>
      </template>
    </ModalWrapper>

  <ModalWrapper :id="'vault-create'">
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


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
