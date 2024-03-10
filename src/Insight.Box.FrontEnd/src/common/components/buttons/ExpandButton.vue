<template>

    <button>
        <expand-icon :isOpen="isExpanded" :initialStyle="ExpandIconStyle.Right"/>
    </button>

</template>

<script setup lang="ts">

import ExpandIcon from "@/common/components/icons/ExpandIcon.vue";
import {ExpandIconStyle} from "@/common/components/icons/ExpandIconStyle";

const props = defineProps({
    isExpanded: {
        type: Boolean,
        required: true
    },
});

// const emit = defineEmits(['expand']);

</script>