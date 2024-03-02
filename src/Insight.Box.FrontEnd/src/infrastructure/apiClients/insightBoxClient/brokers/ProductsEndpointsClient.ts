import type ApiClientBase from "@/infrastructure/apiClients/apiClientBase/ApiClientBase";
import type {Query} from "@/infrastructure/models/query/Query";
import type {ProductFilter} from "@/modules/products/models/ProductFilter";
import type {Product} from "@/modules/products/models/Product";
import type {RequestFormatterService} from "@/infrastructure/apiClients/formatters/RequestFormatterService";
import type {Command} from "@/infrastructure/models/command/Command";

export class ProductsEndpointsClient {
    public client: ApiClientBase;
    public requestFormatterService: RequestFormatterService;

    constructor(client: ApiClientBase, requestFormatterService: RequestFormatterService) {
        this.client = client;
        this.requestFormatterService = requestFormatterService;
    }

    public async getAsync(query: Query<ProductFilter>) {
        const endpointUrl = this.requestFormatterService.addQueryParams('api/products', query);
        return await this.client.getAsync<Array<Product>>(endpointUrl);
    }

    public async createAsync(command: Command<Product>) {
        const endpointUrl =  'api/products';
        return await this.client.postAsync<Product>(endpointUrl, command);
    }
}