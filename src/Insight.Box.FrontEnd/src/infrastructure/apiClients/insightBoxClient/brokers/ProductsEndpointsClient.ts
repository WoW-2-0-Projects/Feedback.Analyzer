import type ApiClientBase from "@/infrastructure/apiClients/apiClientBase/ApiClientBase";
import type {Query} from "@/infrastructure/models/query/Query";
import type {ProductFilter} from "@/modules/products/models/ProductFilter";
import type {Product} from "@/modules/products/models/Product";
import type {RequestFormatterService} from "@/infrastructure/apiClients/formatters/RequestFormatterService";
import {CreateProductCommand} from "@/modules/products/models/CreateProductCommand";

/*
 * Provides products endpoints client functionality
 */
export class ProductsEndpointsClient {
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
    * Gets products
    */
    public async getAsync(query: Query<ProductFilter>) {
        const endpointUrl = this.requestFormatterService.addQueryParams('api/products', query);
        return await this.client.getAsync<Array<Product>>(endpointUrl);
    }

    /*
    * Creates a product
    */
    public async createAsync(command: CreateProductCommand) {
        const endpointUrl = 'api/products';
        return await this.client.postAsync<Product>(endpointUrl, command);
    }
}