<template>

    <Teleport to="body">

        <!-- Modal background -->
        <div v-show="isActiveInternal" :class="isActive ? 'transition duration-1000 modal-bg-overlay-blur' : '' "
             @click="emit('closeModal')">

            <!-- Modal container -->
            <div :class="isActive ? 'absolute-y-center' : 'opacity-0' "
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

import CloseButton from "@/common/components/buttons/CloseButton.vue";
import {ref, watch} from "vue";
import {TimerService} from "@/infrastructure/services/timer/TimerService";

const props = defineProps({
    isActive: {
        type: Boolean,
        default: false
    }
});

const timerService = new TimerService();
const isActiveInternal = ref<boolean>(props.isActive);
const timer = ref<number | null>(null);

watch(() => props.isActive, (isActive) => {
    if (isActive) {
        timer.value = timerService.clearTimer(timer.value);
        timer.value = timerService.setTimer(() => isActiveInternal.value = true, 0);
    } else {
        timer.value = timerService.clearTimer(timer.value);
        timer.value = timerService.setTimer(() => isActiveInternal.value = false, 200);
    }
});

const emit = defineEmits(['closeModal']);


</script>