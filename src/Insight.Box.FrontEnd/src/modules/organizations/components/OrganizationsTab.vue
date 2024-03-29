<template>

    <!-- Organization tab content -->
    <div class="tab pt-10">

        <!-- Organizations search bar -->
        <organizations-search-bar :organizationQuery="organizationsQuery"
                                  @addOrganization="openOrganizationModal(null)"/>

        <!-- Organization a gallery -->
        <div class="mt-4  flex flex-wrap justify-center gap-5">
            <organization-card v-for="organization in organizations"
                               @edit="openOrganizationModal" @delete="onDeleteOrganization"
                               :organization="organization" :key="organization.id"></organization-card>
        </div>


        <!-- Organization create / edit modal -->
        <organization-modal :isActive="organizationModalActive" @closeModal="organizationModalActive = false"
                            @submit="onOrganizationModalSubmit" :isCreate="isCreate"
                            :organization="modalOrganization"/>

        <confirmation-modal text="Are you sure you want to delete it?" :modalOptions="deleteConfirmationDialog"/>

    </div>

</template>

<script setup lang="ts">

import {onBeforeMount, ref} from "vue";
import {DocumentService} from "@/infrastructure/services/document/DocumentService";
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import OrganizationsSearchBar from "@/modules/organizations/components/OrganizationsSearchBar.vue";
import {NotificationSource} from "@/infrastructure/models/notifications/Action";
import {Organization} from "../models/Organization";
import {InsightBoxApiClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/InsightBoxApiClient";
import OrganizationCard from "./OrganizationCard.vue";
import {Query} from "@/infrastructure/models/query/Query";
import {OrganizationFilter} from "@/modules/organizations/models/OrganizationFilter";
import {CreateOrganizationCommand} from "@/modules/organizations/models/CreateOrganizationCommand";
import OrganizationModal from "@/modules/organizations/components/OrganizationModal.vue";
import {UpdateOrganizationCommand} from "@/modules/organizations/models/UpdateOrganizationCommand";
import ConfirmationModal from "@/common/components/confirmationModal/ConfirmationModal.vue";
import {useConfirmDialog} from "@vueuse/core";

const deleteConfirmationDialog = ref(useConfirmDialog());

const insightBoxApiClient = new InsightBoxApiClient();
const documentService = new DocumentService();
const organizations = ref<Array<Organization>>([]);
const organizationsQuery = ref<Query<any>>(new Query(new OrganizationFilter()));

/* Organization modal states  */
const organizationModalActive = ref<boolean>(false);
const organizationsChangeSource = ref<NotificationSource<any>>(new NotificationSource());
const modalOrganization = ref<Organization>(new Organization());


/*Search bar states*/
const isSearchBarLoading = ref<boolean>(false);
const isCreate = ref<boolean>(true);

const organizationDeleteConfirmation = useConfirmDialog();

onBeforeMount(async () => {
    // Set page title
    documentService.setTitle(LayoutConstants.Organizations);
    await loadOrganizationsAsync();
});

const loadOrganizationsAsync = async () => {
    const organizationsResponse = await insightBoxApiClient.organizations.getAsync(organizationsQuery.value);
    if (organizationsResponse.isSuccess) {
        const data = organizationsResponse.response as Organization[];
        organizations.value.push(...data);
    }
};

const openOrganizationModal = async (organization: Organization | null) => {
    if (organization == null) {
        modalOrganization.value = new Organization();
        isCreate.value = true;
    } else {
        modalOrganization.value = JSON.parse(JSON.stringify(organization));
        isCreate.value = false;
    }

    organizationModalActive.value = true;
}

const onOrganizationModalSubmit = async (organization: Organization) => {
    if (isCreate.value)
        await createOrganizationAsync(organization);
    else
        await updateOrganizationAsync(organization);
}

const createOrganizationAsync = async (organization: Organization) => {
    isSearchBarLoading.value = true;

    const createOrganizationCommand = new CreateOrganizationCommand(organization);
    const response = await insightBoxApiClient.organizations.createAsync(createOrganizationCommand);

    if (response.response) {
        organizations.value.push(response.response);
    }

    isSearchBarLoading.value = false;
};

const updateOrganizationAsync = async (organization: Organization) => {
    isSearchBarLoading.value = true;

    const updateOrganizationCommand = new UpdateOrganizationCommand(organization)

    const response = await insightBoxApiClient.organizations.updateAsync(updateOrganizationCommand);

    if (response.response) {
        const organizationId = organizations.value.findIndex(organization => organization.id == response.response?.id);
        if (organizationId > -1)
        {
            organizations.value[organizationId] = response.response;
        }

    }

    isSearchBarLoading.value = false;
}

const onDeleteOrganization = async (organizationId: string) => {

    isSearchBarLoading.value = true;

    const data = (await deleteConfirmationDialog.value.reveal()).data;

    if (data){

        const response = await
            insightBoxApiClient.organizations.deleteAsync(organizationId);

        if (response.isSuccess) {
            const resultId: number = organizations.value.findIndex(organization => organization.id == organizationId);

            if (resultId > -1)
                organizations.value.splice(resultId, 1);
        }
    }

    isSearchBarLoading.value = true;
}

</script>
