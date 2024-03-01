<template>
    <div class=""></div>
</template>

<script setup lang="ts">
import { Organization } from '../models/Organization';

const props = defineProps({
    organization: {
        type: Object as () => Organization,
        required: true
    }
})
</script>