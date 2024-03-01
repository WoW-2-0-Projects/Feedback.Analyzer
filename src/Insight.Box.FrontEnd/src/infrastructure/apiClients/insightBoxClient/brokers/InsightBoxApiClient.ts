import ApiClientBase from "@/infrastructure/apiClients/apiClientBase/ApiClientBase";
import { OrganizationsEndpointsClient } from "./OrganizationsEndpoints";

/*
 * Represents Insight-Box API client
 */
export class InsightBoxApiClient {
    /*
     * API client instance
     */
    private readonly client: ApiClientBase;

    /*
     * Insight Box API base url
     */
    private readonly baseUrl: string;

    constructor() {
        // Set base url
        this.baseUrl = "https://localhost:7239";

        // Create api client instance
        this.client = new ApiClientBase({
            baseURL: this.baseUrl,
            withCredentials: true
        });

        this.organizations = new OrganizationsEndpointsClient(this.client);
    }

    public organizations: OrganizationsEndpointsClient;
}