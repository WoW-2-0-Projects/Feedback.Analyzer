<template>

    <Teleport to="body">

        <!-- Modal background -->
        <div :class="overlayStyles"
             @click="emit('closeModal')" class="h-full w-full theme-modal-transition">

            <!-- Modal container -->
            <div :class="modalStyles"
                 class="fixed h-fit transform top-1/2 w-fit absolute-x-center theme-modal-transition
                     theme-modal-bg modal-border-round theme-modal-border inset-0 z-30 overflow-auto no-scrollbar">

                <div class="w-full h-full relative pt-20" @click.stop>

                    <!-- Modal close button -->
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

import {onBeforeMount, ref, watch} from "vue";
import CloseButton from "@/common/components/buttons/CloseButton.vue";
import {TimerService} from "@/infrastructure/services/timer/TimerService";
import {DocumentService} from "@/infrastructure/services/document/DocumentService";

const timerService = new TimerService();
const documentService = new DocumentService();

const props = defineProps({
    isActive: {
        type: Boolean,
        default: false
    }
});

const emit = defineEmits(['closeModal']);


const timer = ref<number | null>(null);
const overlayStyles = ref<string>('');
const modalStyles = ref<string>('');

onBeforeMount(() => setStyles(true));
watch(() => props.isActive, () => setStyles());

const setStyles = (beforeMount: false) => {
    if (props.isActive) {
        documentService.handleBodyOverflow(props.isActive);
        timer.value = timerService.clearTimer(timer.value);

        overlayStyles.value = '';
        timer.value = timerService.setTimer(() => {
            modalStyles.value = 'absolute-y-center ';
            overlayStyles.value = 'modal-bg-overlay-blur'
        }, 0);
    } else {
        documentService.handleBodyOverflow(props.isActive);
        timer.value = timerService.clearTimer(timer.value);
        modalStyles.value = 'opacity-0 ';

        if(beforeMount) {
            overlayStyles.value = 'hidden ';
        } else {
            timer.value = timerService.clearTimer(timer.value);
            timer.value = timerService.setTimer(() => overlayStyles.value = 'hidden', 200);
        }
    }
}


</script>