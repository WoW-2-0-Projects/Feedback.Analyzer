<template>

    <Teleport to="body">

        <!-- Modal background overlay -->
        <div class="z-30 modal-bg-overlay-blur" @click="emit('closeModal')"
             :class="{'animate-backgroundFadeIn': props.isActive, 'animate-backgroundFadeOut': !props.isActive,
                      'hidden': hideModal}">

            <!-- Modal container -->
            <div
                class="fixed inset-0 w-fit h-fit z-40 top-1/2 overflow-auto no-scrollbar
                    theme-modal-bg modal-border-round theme-modal-border" @click.stop
                :class="props.isActive ? 'animate-fadeIn' : 'animate-fadeOut'"
            >
                <div class="w-full h-full relative pt-20">

                    <close-button class="absolute top-5 right-5" @click="emit('closeModal')"/>

                    <!-- Modal header -->
                    <div class="absolute top-6 absolute-x-center">
                        <slot name="header"/>
                    </div>

                    <!-- Modal content -->
                    <slot name="content"/>
                </div>
            </div>
        </div>

    </Teleport>

</template>

<script setup lang="ts">

import {onBeforeMount, onBeforeUnmount, ref, watch} from "vue";
import CloseButton from "@/common/components/buttons/CloseButton.vue";
import {DocumentService} from "@/infrastructure/services/document/DocumentService";
import {TimerService} from "@/infrastructure/services/timer/TimerService";

const timerService = new TimerService();
const documentService = new DocumentService();

const props = defineProps({
    isActive: {
        type: Boolean,
        default: false
    }
});

const timer = ref<number>(0);
const hideModal = ref<boolean>(!props.isActive);
const emit = defineEmits(['closeModal']);

onBeforeMount(() => setStyles());
watch(() => props.isActive, () => setStyles());
onBeforeUnmount(() => timerService.clearTimer(timer.value));

const setStyles = () => {
    documentService.handleBodyOverflow(props.isActive);
    timerService.setTimer(() => hideModal.value = !props.isActive, props.isActive ? 0 : 300);
}

</script>