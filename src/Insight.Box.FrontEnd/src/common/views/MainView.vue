<template>

    <!-- View content -->
    <div class="view theme-gradient-bg content-padding">

        <!-- Site header -->
        <main-header/>

        <router-view/>
    </div>

</template>
<script setup lang="ts">
import MainHeader from "@/common/components/mainHeader/MainHeader.vue";
</script>