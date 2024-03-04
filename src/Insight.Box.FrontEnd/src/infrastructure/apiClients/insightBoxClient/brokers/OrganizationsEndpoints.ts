import type ApiClientBase from "../../apiClientBase/ApiClientBase";
import { Organization } from "@/modules/organizations/models/Organization";
import type {RequestFormatterService} from "@/infrastructure/apiClients/formatters/RequestFormatterService";
import type {Query} from "@/infrastructure/models/query/Query";
import type {OrganizationFilter} from "@/modules/organizations/models/OrganizationFilter";

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
}