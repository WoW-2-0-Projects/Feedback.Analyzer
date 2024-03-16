import ApiClientBase from "@/infrastructure/apiClients/apiClientBase/ApiClientBase";
import {ProductsEndpointsClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/ProductsEndpointsClient";
import {RequestFormatterService} from "@/infrastructure/apiClients/formatters/RequestFormatterService";
import {
    OrganizationsEndpointsClient
} from "@/infrastructure/apiClients/insightBoxClient/brokers/OrganizationsEndpoints";
import {AuthEndpointsClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/AuthEndpointsClient";
import {ClientsEndpointsClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/ClientsEndpointsClient";
import {
    WorkflowsEndpointsClient
} from "@/infrastructure/apiClients/insightBoxClient/brokers/WorkflowsEndpointsClient";

/*
 * Provides Insight Box API client functionality
 */
export class InsightBoxApiClient {
    /*
     * API client instance
     */
    private readonly client: ApiClientBase;

    /*
     * Request formatter service
     */
    private readonly requestFormatterService: RequestFormatterService;

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

        // Create request formatter service instance
        this.requestFormatterService = new RequestFormatterService();

        // Initialize endpoint clients
        this.organizations = new OrganizationsEndpointsClient(this.client, this.requestFormatterService);
        this.products = new ProductsEndpointsClient(this.client, this.requestFormatterService);
        this.workflows = new WorkflowsEndpointsClient(this.client, this.requestFormatterService);
        this.auth = new AuthEndpointsClient(this.client);
        this.clients = new ClientsEndpointsClient(this.client);
    }

    public organizations: OrganizationsEndpointsClient;
    public products: ProductsEndpointsClient;
    public workflows: WorkflowsEndpointsClient;
    public readonly clients: ClientsEndpointsClient;
    public readonly auth: AuthEndpointsClient;
}