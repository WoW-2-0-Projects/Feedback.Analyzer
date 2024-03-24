import type ApiClientBase from "../../apiClientBase/ApiClientBase";
import {Organization} from "@/modules/organizations/models/Organization";
import type {RequestFormatterService} from "@/infrastructure/apiClients/formatters/RequestFormatterService";
import type {Query} from "@/infrastructure/models/query/Query";
import type {OrganizationFilter} from "@/modules/organizations/models/OrganizationFilter";
import type {CreateOrganizationCommand} from "@/modules/organizations/models/CreateOrganizationCommand";
import type {UpdateOrganizationCommand} from "@/modules/organizations/models/UpdateOrganizationCommand";

/*
 * Provides organization endpoints client functionality
 */
export class OrganizationsEndpointsClient {
    /*
    * API client instance
    */
    private readonly client: ApiClientBase;

    /*
     * Request formatter service
    */
    private readonly requestFormatterService: RequestFormatterService;

    constructor(client: ApiClientBase, requestFormatterService: RequestFormatterService) {
        this.client = client;
        this.requestFormatterService = requestFormatterService;
    }

    /*
     * Get organizations
     */
    public async getAsync(query: Query<OrganizationFilter>) {
        const endpointUrl = this.requestFormatterService.addQueryParams('api/organizations', query);
        return await this.client.getAsync<Organization[]>(endpointUrl);
    }

    /*
     * Create an organization
     */
    public async createAsync(command: CreateOrganizationCommand) {
        const endpointUrl = 'api/organizations';

        return await this.client.postAsync<Organization>(endpointUrl, command);
    }

    /*
     * Update an organization
     */
    public async updateAsync(command: UpdateOrganizationCommand) {
        const endpointUrl = `api/organizations`;

        return await this.client.putAsync<Organization>(endpointUrl, command);

    }

    /*
     * Delete an organization
     */
    public async deleteAsync(organizationId : string) {

        const endpointUrl = `api/organizations/${organizationId}`;

        return await this.client.deleteAsync(endpointUrl);
    }
}