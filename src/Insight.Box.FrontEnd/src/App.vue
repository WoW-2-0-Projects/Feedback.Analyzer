<template>

    <home-view v-if="!authService.isLoggedIn()"/>
    <main-view v-else/>
    <auth-modal :isActive="isAuthModalActive" @closeModal="isAuthModalActive = false"/>

</template>

<script setup lang="ts">
import MainView from "@/common/views/MainView.vue";
import AuthModal from "@/modules/accounts/companents/AuthModal.vue";
import {onBeforeMount, ref} from "vue";
import {AuthenticationService} from "@/modules/accounts/services/AuthenticationService";
import HomeView from "@/common/views/HomeView.vue";

const authService = new AuthenticationService();
const isAuthModalActive = ref<boolean>(false);

onBeforeMount(async () => {
    // Set account
    await authService.setCurrentUser();
    isAuthModalActive.value = !authService.isLoggedIn();
});

</script>