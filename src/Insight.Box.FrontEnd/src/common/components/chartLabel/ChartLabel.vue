<template>

    <div class="flex items-center justify-center gap-2">
        <div class="h-3 w-3 rounded-full" :class="`bg-[${color}]`"></div>
        <span class="text-sm">{{text}}</span>
    </div>

</template>

<script setup lang="ts">


const props = defineProps({
    text: {
        type: String,
        required: true
    },
    color: {
        type: String,
        default: '#F3BD93'
    }
});

</script>