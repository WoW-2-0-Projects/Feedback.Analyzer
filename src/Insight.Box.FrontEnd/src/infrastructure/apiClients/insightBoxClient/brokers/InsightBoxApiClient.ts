import ApiClientBase from "@/infrastructure/apiClients/apiClientBase/ApiClientBase";
import {ProductsEndpointsClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/ProductsEndpointsClient";
import {RequestFormatterService} from "@/infrastructure/apiClients/formatters/RequestFormatterService";
import {PromptsEndpointsClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/PromptsEndpointsClient";

/*
 * Represents Insight-Box API client
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
        this.products = new ProductsEndpointsClient(this.client, this.requestFormatterService);
        this.prompts = new PromptsEndpointsClient(this.client, this.requestFormatterService);
    }

    public products: ProductsEndpointsClient;
    public prompts: PromptsEndpointsClient;
}