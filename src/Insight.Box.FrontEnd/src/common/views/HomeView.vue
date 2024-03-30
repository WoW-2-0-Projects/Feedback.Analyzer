<template>

    <h3>Landing page</h3>

</template>

<script setup lang="ts">

import {onBeforeMount} from "vue";
import {useRouter} from "vue-router";
import {AuthenticationService} from "@/modules/accounts/services/AuthenticationService";

const authService = new AuthenticationService();
const router = useRouter();

onBeforeMount(() => {
    if (authService.isLoggedIn())
        router.push({name: "Workflows"});
});


</script>