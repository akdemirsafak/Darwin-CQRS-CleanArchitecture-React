<template>
      <!-- <v-app>
        <v-app-bar class="mb-10"> -->
  <v-toolbar app class="bg-blue-lighten-5">
      <span class="hidden-sm-and-up">
        <v-toolbar-side-icon @click="sidebar = !sidebar">
        </v-toolbar-side-icon>
      </span>   

      <v-toolbar-title >
        <router-link to="/" tag="span" style="cursor: pointer" class="text-decoration-none text-deep-orange-lighten-2 d-flex"  >
              <img src="@/assets/darwin_removed_bg.png" class="rounded" style="width: 96px;"  />

        </router-link>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-toolbar-items class="hidden-xs-only">
        <v-btn
        outlined
        color="black"
        
          flat
          v-for="item in menuItems"
          :key="item.title"
          :to="item.path">
          <v-icon left dark>{{ item.icon }}</v-icon>
          {{ item.title }}
        </v-btn>
      </v-toolbar-items>
    </v-toolbar>
        <!-- </v-app-bar>
      </v-app> -->
      

</template>

<script>

export default {
  name: "App",
  data(){
    return {
      appTitle: 'Darwin Music App',
      sidebar: false,
      menuItems: [
         { "path": "/", "title": "Home Page","icon":"mdi-home" },
            { "path":"/Category","title":"Category","icon":"mdi-shape-outline" },
            { "path":"/Music","title":"Music","icon":"mdi-music-note" },
            { "path":"/Mood","title":"Mood","icon":"mdi-heart-broken" },
     ]
    }
  },
};
</script>