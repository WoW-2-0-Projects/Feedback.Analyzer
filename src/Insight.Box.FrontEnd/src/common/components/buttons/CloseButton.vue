<template>

    <app-button :type="ActionType.Danger" :layout="ButtonLayout.Circle" icon="fas fa-times"/>

</template>

<script setup lang="ts">

import AppButton from "@/common/components/appButton/AppButton.vue";
import {ButtonLayout} from "@/common/components/appButton/ButtonLayout";
import {ActionType} from "@/common/components/actions/ActionType";

</script>