<template>

    <main-view/>

    <auth-modal :isActive="isAuthModalActive" @closeModal="isAuthModalActive = false"/>

</template>

<script setup lang="ts">
import MainView from "@/common/views/MainView.vue";
import AuthModal from "@/modules/accounts/companents/AuthModal.vue";
import {ref} from "vue";
import {AuthenticationService} from "@/modules/accounts/services/AuthenticationService";

const authService = new AuthenticationService();
const isAuthModalActive = ref<boolean>(!authService.isLoggedIn());
</script>