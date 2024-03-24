<template>

    <ModalBase :isActive="modalOptions.isRevealed" @closeModal="modalOptions.confirm(false)">
        <template v-slot:header>
            <h1 class="text-primaryContentColor text-xl"> {{ text }} </h1>
        </template>

        <!-- Organization modal content-->
        <template v-slot:content>
            <div class="min-w-[700px] modal-content-padding modal-content-layout">
                <!-- Modal actions -->
                <div class="flex gap-10">
                    <app-button :type="ActionType.Secondary" class="w-full" :text="LayoutConstants.Cancel"
                                @click="modalOptions.confirm(false)"/>
                    <app-button :type="ActionType.Primary" class="w-full" :text="LayoutConstants.Submit"
                                :role="ButtonRole.Submit" @click="modalOptions.confirm(true)"/>
                </div>
            </div>
        </template>

    </ModalBase>

</template>

<script setup lang="ts">

import {type UseConfirmDialogReturn} from '@vueuse/core'
import ModalBase from "@/common/components/modalBase/ModalBase.vue";
import {type PropType, watch} from "vue";
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import {ButtonRole} from "@/common/components/appButton/ButtonRole";
import AppButton from "@/common/components/appButton/AppButton.vue";
import {ActionType} from "@/common/components/actions/ActionType";

const props = defineProps({
    text: {
        type: String,
        required: true
    },
    modalOptions: {
        type: Object as PropType<UseConfirmDialogReturn<any, any, any>>,
        required: true
    }
});


</script>