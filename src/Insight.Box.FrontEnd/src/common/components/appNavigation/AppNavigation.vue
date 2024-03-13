<template>

    <div class="flex gap-10 text-md text-textColorSecondary">
        <nav-item v-for="route in routerService.getRoutes()" :key="route.name" :route="route"/>
    </div>

</template>

<script setup lang="ts">

import {RouterService} from "@/infrastructure/services/router/RouterService";
import NavItem from "@/common/components/appNavigation/NavItem.vue";
const routerService = new RouterService();

</script>