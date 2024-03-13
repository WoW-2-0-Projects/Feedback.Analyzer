<template>

    <div class="h-10 w-10 flex items-center justify-center rounded-full bg-secondaryContentColor bg-opacity-20">
        <img v-if="image" :src="image" alt="User avatar"/>
        <div v-else class="uppercase font-semibold text-secondaryContentColor">{{ formattedFallbackText }}</div>
    </div>

</template>

<script setup lang="ts">

import {computed, type PropType} from "vue";
import type {RouteRecordRaw} from "vue-router";

const props = defineProps({
    image: {
        type: String as PropType<string>,
    },
    fullName: {
        type: String as PropType<string>,
        required: true
    }
});

const formattedFallbackText = computed(() => {
    return props.fullName.split(' ').map((name: string) => name[0]).join('');
});

</script>