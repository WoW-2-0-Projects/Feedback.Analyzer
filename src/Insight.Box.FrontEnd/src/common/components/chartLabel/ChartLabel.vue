<template>

    <div class="flex items-center justify-center gap-2">
        <div class="h-3 w-3 rounded-full" :class="`bg-[${color}]`"></div>
        <span :class="textStyles">{{ text }}</span>
    </div>

</template>

<script setup lang="ts">

import {ActionComponentSize} from "@/common/components/formInput/ActionComponentSize";
import {computed, type PropType} from "vue";

const props = defineProps({
    text: {
        type: String,
        required: true
    },
    color: {
        type: String,
        default: '#F3BD93'
    },
    size: {
        type: Number as PropType<ActionComponentSize>,
        default: ActionComponentSize.Small
    }
});

const textStyles = computed(() => {
    let styles = '';

    console.log('test', props.size);

    switch (props.size) {
        case ActionComponentSize.ExtraSmall:
            styles = ' text-xs';
            break;
        case ActionComponentSize.Small:
            styles = ' text-sm';
            break;
        case ActionComponentSize.Medium:
            styles = ' text-base';
            break;
    }

    return styles;
});

</script>