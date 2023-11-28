import { createRouter, createWebHashHistory } from 'vue-router'
import { authGuard, authSettled } from '@bcwdev/auth0provider-client'

function loadPage(page) {
  return () => import(`./pages/${page}.vue`)
}

const routes = [
  {
    path: '/',
    name: 'Home',
    component: loadPage('HomePage')
  },
  {
    path: '/about',
    name: 'About',
    component: loadPage('AboutPage')
  },
  {
    path: '/account',
    name: 'Account',
    component: loadPage('AccountPage'),
    beforeEnter: authGuard
  },
  {
    path: '/profile/:profileId',
    name: 'Profile',
    component: loadPage('ProfilePage')
  },
  // NOTE: we can create 2 different paths to the SAME page, that is awesome for certain cases, such as the one we deal with here, which is getting to the same page with 2 different URLs, one for account, one for profile.
  // {
  //   path: '/profile/vault/:vaultId',
  //   name: 'Vault',
  //   component: loadPage('VaultPage')
  // },

  // NOTE: this is the required syntax to fix the issue where refreshing on a private vault will kick you out of your own vault.

  {
    path: '/vault/:vaultId',
    name: 'Vault',
    component: loadPage('VaultPage'),
    beforeEnter: authSettled
  }
]

export const router = createRouter({
  linkActiveClass: 'router-link-active',
  linkExactActiveClass: 'router-link-exact-active',
  history: createWebHashHistory(),
  routes
})
