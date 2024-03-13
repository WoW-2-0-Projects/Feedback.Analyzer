<template>

    <!-- View content -->
    <div class="view content-padding bg-primaryColor">
        <main-header/>
        <router-view/>
    </div>

</template>
<script setup lang="ts">
import MainHeader from "@/common/components/mainHeader/MainHeader.vue";
</script>