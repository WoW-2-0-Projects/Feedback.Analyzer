<template>

    <div class="py-1 px-4 rounded-full bg-secondaryContentColor text-secondaryContentColor uppercase"
        :class="isCurrentRouteSelected ? 'bg-opacity-20' : 'bg-opacity-0 text-opacity-50' "
    >
        <router-link class="text-base" :to="route.path">{{ route.name }}</router-link>
    </div>

</template>

<script setup lang="ts">

import {onMounted, type PropType, ref} from "vue";
import {RouterService} from "@/infrastructure/services/router/RouterService";
import type {RouteLocationNormalized, RouteRecordRaw} from "vue-router";

const routerService = new RouterService();
const isCurrentRouteSelected = ref<boolean>();

const props = defineProps({
    route: {
        type: Object as PropType<RouteRecordRaw>,
        required: true
    }
});

// const isCurrentRouteActive = computed(() => {
//
// });

onMounted(() => {
    routerService.addRouteChangeListener(handleRouteChange);
});

const handleRouteChange = (route: RouteLocationNormalized) => {
    isCurrentRouteSelected.value = route.path === props.route.path;
}

</script>
