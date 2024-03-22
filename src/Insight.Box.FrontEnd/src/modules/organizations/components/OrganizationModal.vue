<template>

  <ModalBase :isActive="props.isActive" @closeModal="emit('closeModal')">
      <template v-slot:header>
        <h1 class="text-primaryContentColor text-xl">
          {{
            isCreate ? LayoutConstants.CreateOrganization
                : LayoutConstants.EditOrganization + ' ' + organization.name
          }}
        </h1>
      </template>



<!-- Organization modal content-->
      <template v-slot:content>
        <div class="modal-content-padding modal-content-layout">

          <form class="flex flex-col gap-10" @submit.prevent="onSubmit">

            <form-input v-model="organization.name" :label="LayoutConstants.OrganizationName"
                        :placeholder="LayoutConstants.EnterOrganizationName"/>

            <form-text-area class="h-[350px]" v-model="organization.description" :label="LayoutConstants.OrganizationDescription"
                            :placeholder="LayoutConstants.EnterOrganizationDescription"
            />

            <!-- Modal actions -->
            <div class="flex gap-10">
                        <app-button :type="ActionType.Secondary" class="w-full" :text="LayoutConstants.Cancel"
                                    @click="emit('closeModal')"/>
                        <app-button :type="ActionType.Primary" class="w-full" :text="LayoutConstants.Submit"
                                    :role="ButtonRole.Submit" @click="onSubmit"/>
                    </div>

          </form>

        </div>

      </template>

  </ModalBase>

</template>


<script setup lang="ts">



import ModalBase from "@/common/components/modalBase/ModalBase.vue";
import {defineEmits, type PropType, ref, watch} from "vue";
import {Organization} from "@/modules/organizations/models/Organization";
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import FormInput from "@/common/components/formInput/FormInput.vue";
import {ButtonRole} from "@/common/components/appButton/ButtonRole";
import AppButton from "@/common/components/appButton/AppButton.vue";
import FormTextArea from "@/common/components/formTextArea/FormTextArea.vue";
import {ActionType} from "@/common/components/actions/ActionType";


const props = defineProps({
  organization: {
    type: Object as PropType<Organization>,
    default: new Organization()
  },
  isActive: {
    type: Boolean,
    default: false
  },
  isCreate: {
    type: Boolean,
    default: true
  }
});

const emit = defineEmits<{
  (e: 'closeModal'): void
  (e: 'submit', organization: Organization): void
}>();

const onSubmit = async () => {
  emit('submit', props.organization);
  emit('closeModal');
}

</script>

