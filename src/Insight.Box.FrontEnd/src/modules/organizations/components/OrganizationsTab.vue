<template>

    <!-- Organization view content -->
    <div class="tab pt-10">

        <!-- Organizations search bar -->
        <organizations-search-bar/>

        <!-- Organizations gallery -->
        <infinite-scroll @onScroll="onScroll"
                         :contentChangeSource="organizationsChangeSource"
                         class="grid w-full px-20 gap-x-5 gap-y-10 grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 theme-bg-primary">

            <!-- Listing card -->

        </infinite-scroll>

    </div>

</template>

<script setup lang="ts">

import {onMounted, onBeforeMount, ref} from "vue";
import {DocumentService} from "@/infrastructure/services/document/DocumentService";
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import {OrganizationFilter} from "@/modules/organizations/models/OrganizationFilter";
import OrganizationsSearchBar from "@/modules/organizations/components/OrganizationsSearchBar.vue";
import InfiniteScroll from "@/common/components/infiniteScroll/InfiniteScroll.vue";
import {NotificationSource} from "@/infrastructure/models/notifications/Action";
import { Organization } from "../models/Organization";
import { InsightBoxApiClient } from "@/infrastructure/apiClients/insightBoxClient/brokers/InsightBoxApiClient";
import { FilterPagination } from "@/common/FilterPagination";

const insightBoxApiClient = new InsightBoxApiClient();
const documentService = new DocumentService();
const organizations = ref<Array<Organization>>([]);
const organizationsFilter = ref<FilterPagination>(new FilterPagination());


onBeforeMount(async () => {
    // Set page title
    documentService.setTitle(LayoutConstants.Organizations);
    await loadOrganizationsAsync();
});

const loadOrganizationsAsync = async () => {
    const organizationsResponse = await insightBoxApiClient.organizations.getAsync(organizationsFilter.value);
    if(organizationsResponse.isSuccess) {
        const data = organizationsResponse.response as Organization[];
        console.log(data);
        organizations.value.push(...data);
    } else{
        console.log("Something is wrong");
    }
};

/*
 * Change source of organizations to re-calculate layout
 */
const organizationsChangeSource = ref<NotificationSource>(new NotificationSource());

/*
 * Scroll event handler
 */
const onScroll = async () => {
};

</script>