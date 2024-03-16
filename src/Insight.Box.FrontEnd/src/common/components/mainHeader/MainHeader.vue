<template>

    <div class="h-24 w-full flex items-center justify-between">

        <main-logo/>

        <app-navigation/>

        <div/>

        <avatar-circle :fullName="accountStore.account.client.firstName + ' ' + accountStore.account.client.lastName"/>

    </div>

</template>

<script setup lang="ts">

import MainLogo from "@/common/components/mainLogo/MainLogo.vue";
import AppNavigation from "@/common/components/appNavigation/AppNavigation.vue";
import AvatarCircle from "@/common/components/avatarCircle/AvatarCircle.vue";
import {useAccountStore} from "@/infrastructure/stores/AccountStore";

const accountStore = useAccountStore();

</script>