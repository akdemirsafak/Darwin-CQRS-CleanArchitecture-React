// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  devtools: { enabled: true },
  css : [
    'vuetify/styles/main.css',
    '@mdi/font/css/materialdesignicons.css'
  ],
  build:{
    transpile:['vuetify'],
  },
  modules: ['nuxt-icon'],
  
});
