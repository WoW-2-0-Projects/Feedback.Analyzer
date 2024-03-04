<template>

    <app-button :type="ButtonType.Danger" :layout="ButtonLayout.Circle" icon="fas fa-times"/>

</template>

<script setup lang="ts">

import AppButton from "@/common/components/appButton/AppButton.vue";
import {ButtonType} from "@/common/components/appButton/ButtonType";
import {ButtonLayout} from "@/common/components/appButton/ButtonLayout";

</script>