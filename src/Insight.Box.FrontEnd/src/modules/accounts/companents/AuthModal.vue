<template>

    <ModalBase :isActive="props.isActive" @closeModal="closeModal">

        <template v-slot:header>
            <h1 class="text-primaryContentColor text-xl">{{ modalTitle }}</h1>
        </template>

        <!-- Product modal content -->
        <template v-slot:content>

            <div class="modal-content-padding modal-content-layout">
                <form class="flex flex-col gap-10" @submit.prevent="onSubmit">

                    <!-- Modal inputs -->

                    <form-input v-if="!isSignIn" v-model="signUpDetails.firstName" :label="LayoutConstants.FirstName"
                                :placeholder="LayoutConstants.EnterFirstName"/>

                    <form-input v-if="!isSignIn" v-model="signUpDetails.lastName" :label="LayoutConstants.LastName"
                                :placeholder="LayoutConstants.EnterLastName"/>

                    <form-input v-model="signInDetails.emailAddress" :label="LayoutConstants.EmailAddress"
                                :placeholder="LayoutConstants.EnterEmailAddress"/>

                    <form-input v-if="foundUserName" v-model="signInDetails.password" :label="LayoutConstants.Password"
                                :placeholder="LayoutConstants.EnterPassword" :type="FormInputType.Password"/>

                    <!-- Modal actions -->
                    <div class="flex gap-10">
                        <app-button :type="ButtonType.Secondary" class="w-full" :text="LayoutConstants.Cancel"
                                    @click="closeModal"/>
                        <app-button :type="ButtonType.Primary" class="w-full" :text="LayoutConstants.Submit"
                                    :role="ButtonRole.Submit" @click="onSubmit"/>
                    </div>

                </form>
            </div>

        </template>

    </ModalBase>

</template>

<script setup lang="ts">

import {defineEmits, ref, watch} from 'vue';
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import FormInput from "@/common/components/formInput/FormInput.vue";
import ModalBase from "@/common/components/modalBase/ModalBase.vue";
import {SignUpDetails} from "@/modules/accounts/models/SignUpDetails";
import {SignInDetails} from "@/modules/accounts/models/SignInDetails";
import {FormInputType} from "@/common/components/formInput/FormInputType";
import {InsightBoxApiClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/InsightBoxApiClient";
import {AuthenticationService} from "@/modules/accounts/services/AuthenticationService";
import {ButtonType} from "@/common/components/appButton/ButtonType";
import {ButtonRole} from "@/common/components/appButton/ButtonRole";
import AppButton from "@/common/components/appButton/AppButton.vue";

const  authService = new AuthenticationService();
const insightBoxApiClient = new InsightBoxApiClient();

const props = defineProps({
    isActive: {
        type: Boolean,
        default: false
    }
});


const emit = defineEmits<{
    (e: 'submit', signInDetails: SignInDetails): void
    (e: 'closeModal'): void
}>();

const signUpDetails = ref<SignUpDetails>(new SignUpDetails());
const signInDetails = ref<SignInDetails>(new SignInDetails());
const modalTitle = ref<string>(LayoutConstants.WelcomeToMessage);
const isSignIn = ref<boolean>(true);
const foundUserName = ref<string>('');

const closeModal = () => {
    // Reset values;
    if(props.isActive == true)
         emit('closeModal');
}

const checkIfEmailAddressExists = async (emailAddress: string) => {
    foundUserName.value = '';
    const response = await insightBoxApiClient.clients.checkByEmailAsync(emailAddress);
    if(response.response)
        foundUserName.value = response.response;

    return foundUserName.value;
}

const onSubmit = async () => {
    // If user tried to sign in, but email address not found
    if(isSignIn.value && !foundUserName.value && !(await checkIfEmailAddressExists(signInDetails.value.emailAddress))) {
        isSignIn.value = false;
        modalTitle.value = LayoutConstants.WelcomeToMessage;
        return;
    }

    // If user tried to sign up, but email already exists
    if(!isSignIn.value && await checkIfEmailAddressExists(signInDetails.value.emailAddress)) {
        isSignIn.value = true;
        modalTitle.value = LayoutConstants.WelcomeBackMessage + foundUserName.value;
        return;
    }

    console.log(isSignIn.value)
    if(isSignIn.value ) {
        await signIn();
    }
    else
        signUp();


}

const signIn = async () => {
    const response = await authService.signInAsync(signInDetails.value)
    if(response){
        closeModal()
    }
}
const signUp = () => {

}

</script>