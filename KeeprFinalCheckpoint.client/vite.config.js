import vue from '@vitejs/plugin-vue'
import { defineConfig } from 'vite'

// https://vitejs.dev/config/
export default defineConfig({
  base: '/keepr',
  plugins: [vue()],
  build: {
    outDir: '../{{KeeprFinalCheckpoint}}/wwwroot',
    sourcemap: false
  },
  server: {
    port: 8080
  }
})
