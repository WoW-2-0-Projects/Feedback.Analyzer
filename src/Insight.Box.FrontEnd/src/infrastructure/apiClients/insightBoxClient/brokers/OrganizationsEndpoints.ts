import type ApiClientBase from "../../apiClientBase/ApiClientBase";
import { Organization } from "@/modules/organizations/models/Organization";
import type {RequestFormatterService} from "@/infrastructure/apiClients/formatters/RequestFormatterService";
import type {Query} from "@/infrastructure/models/query/Query";
import type {OrganizationFilter} from "@/modules/organizations/models/OrganizationFilter";

export class OrganizationsEndpointsClient {
    public client: ApiClientBase;
    public requestFormatterService: RequestFormatterService;

    constructor(client: ApiClientBase, requestFormatterService: RequestFormatterService) {
        this.client = client;
        this.requestFormatterService = requestFormatterService;
    }

    public async getAsync(query: Query<OrganizationFilter>) {
        const endpointUrl = this.requestFormatterService.addQueryParams('api/organizations', query);
        // const endpointUrl = `api/organizations?OrganizationFilter.PageSize=${filter.pageSize}&OrganizationFilter.PageToken=${filter.pageToken}`;
        return await this.client.getAsync<Organization[]>(endpointUrl);
    }
}