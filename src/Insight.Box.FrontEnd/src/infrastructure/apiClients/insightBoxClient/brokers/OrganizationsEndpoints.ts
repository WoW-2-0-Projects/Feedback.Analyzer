import type { FilterPagination } from "@/common/FilterPagination";
import type ApiClientBase from "../../apiClientBase/ApiClientBase";
import { Organization } from "@/modules/organizations/models/Organization";
import { ApiResponse } from "../../apiClientBase/ApiResponse";

export class OrganizationsEndpointsClient {
    public client: ApiClientBase;

    constructor(client: ApiClientBase){
        this.client = client;
    }

    public async getAsync(filter: FilterPagination) {
        const endpointUrl = `api/organizations?OrganizationFilter.PageSize=${filter.pageSize}&OrganizationFilter.PageToken=${filter.pageToken}`;

        return await this.client.getAsync<Organization[]>(endpointUrl);
    }
}