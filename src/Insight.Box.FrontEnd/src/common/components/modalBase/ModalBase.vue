<template>

    <Teleport to="body">

        <!-- Modal background -->
        <div :class="isActive ? 'modal-bg-overlay-blur' : ''" @click="emit('closeModal')">

            <!-- Modal container -->
            <div :class="isActive ? ' absolute-y-center' : ' opacity-0'"
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

const props = defineProps({
    isActive: {
        type: Boolean,
        default: false
    }
});


const emit = defineEmits(['closeModal']);

</script>