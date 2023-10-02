// import { colors } from 'vuetify/lib/util/colors';
import {createVuetify} from 'vuetify';
import * as components from 'vuetify/components';
import * as directives from 'vuetify/directives';

export default defineNuxtPlugin((nuxt)=>{
    const vuetify=createVuetify({
        ssr:true,
        components,
        directives,
        theme:{
            themes:{
                light:{
                    dark:false,
                    colors:{
                        
                    },
                },
            }
        },
    });
    nuxt.vueApp.use(vuetify);
})