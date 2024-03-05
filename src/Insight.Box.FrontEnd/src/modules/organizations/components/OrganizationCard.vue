<template>
    <div class="bg-[#5d6285] flex flex-col items-center justify-around pt-3 rounded-3xl w-[290px] h-[192px] shadow-lg">
        <!-- organization image -->
        <img class="rounded-full w-24 h-24 bg-[#878aa3]"
            src="https://www.logolynx.com/images/logolynx/s_58/585472b8c0bbdc5171ac72fa9478131e.jpeg">
        <h3 class="text-white">{{ organization.name }}</h3>
        <div class="w-full pl-3 text-white">
            <span>{{ organization.productsCount }} Products</span>
        </div>
    </div>
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