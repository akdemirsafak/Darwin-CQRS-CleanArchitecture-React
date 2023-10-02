
import Navbar from '~/components/global/navbar.vue';
<template>
<div >
    <header>
        <AppNavbar></AppNavbar>
    </header>
    <main class="w-75 mx-auto">
        <!-- <slot/> -->
        <NuxtPage></NuxtPage>
    </main>
    
    <footer>
        <AppFooter></AppFooter>
    </footer>

</div>
</template>

<script>
import AppNavbar from '@/components/global/AppNavbar.vue';
import AppFooter from '@/components/global/AppFooter.vue';

export default{
    components:{
        AppNavbar,AppFooter
    }
}
</script>
