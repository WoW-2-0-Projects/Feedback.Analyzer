<template>

    <div class="h-20 w-full content-padding flex items-center justify-center">

        <template>

            <!-- Site logo -->

            <!-- Site title -->

        </template>

        <!-- Site navigation -->
        <div class="flex gap-10 text-md text-textColorSecondary">

            <router-link :to="RouteName.Organizations">Dashboard</router-link>
            <router-link :to="RouteName.Prompts">Prompts</router-link>
            <router-link :to="RouteName.Organizations">Organizations</router-link>
            <router-link :to="RouteName.Products">Products</router-link>
            <router-link :to="RouteName.Organizations">Workflows</router-link>

        </div>

        <!-- Notifications -->

        <!-- Profile menu -->

    </div>

</template>
<script setup lang="ts">
import {RouteName} from "@/infrastructure/router/RouteName.js";
</script>