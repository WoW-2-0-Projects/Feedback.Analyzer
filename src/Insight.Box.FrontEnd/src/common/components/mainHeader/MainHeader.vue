<template>

    <div class="h-24 w-full flex items-center justify-between">

        <main-logo/>

        <app-navigation/>

        <div/>

        <avatar-circle fullName="john doe"/>

    </div>

</template>

<script setup lang="ts">

import MainLogo from "@/common/components/mainLogo/MainLogo.vue";
import AppNavigation from "@/common/components/appNavigation/AppNavigation.vue";
import AvatarCircle from "@/common/components/avatarCircle/AvatarCircle.vue";

</script>