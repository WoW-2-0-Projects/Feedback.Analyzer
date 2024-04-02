import type ApiClientBase from "@/infrastructure/apiClients/apiClientBase/ApiClientBase";
import type {RequestFormatterService} from "@/infrastructure/apiClients/formatters/RequestFormatterService";
import type {Query} from "@/infrastructure/models/query/Query";
import type {PromptFilter} from "@/modules/prompts/models/PromptFilter";
import type {AnalysisPrompt} from "@/modules/prompts/models/AnalysisPrompt";
import type {PromptCategoryFilter} from "@/modules/prompts/models/PromptCategoryFilter";
import type {AnalysisPromptCategory} from "@/modules/prompts/models/AnalysisPromptCategory";
import type {CreatePromptCommand} from "@/modules/prompts/models/CreatePromptCommand";
import type {PromptExecutionResult} from "@/modules/prompts/models/PromptExecutionResult";

/**
 * Provides prompts endpoints client functionality
 */
export  class PromptsEndpointsClient {
    public  client: ApiClientBase;
    public requestFormatterService: RequestFormatterService;

    constructor(client: ApiClientBase, requestFormatterService: RequestFormatterService) {
        this.client = client;
        this.requestFormatterService = requestFormatterService;
    }

    /**
     * Asynchronously retrieves prompts based on the provided query.
     */
    public async getAsync(query: Query<PromptFilter>) {
        const endpointUrl = this.requestFormatterService.addQueryParams('api/prompts', query);
        return await this.client.getAsync<Array<AnalysisPrompt>>(endpointUrl);
    }

    /**
     * Get prompt by id
     */
    public async getByIdAsync(id: string) {
        const endpointUrl = `api/prompts/${id}`;

        return await this.client.getAsync<AnalysisPrompt>(endpointUrl);
    }

    /**
     * Asynchronously retrieves prompt categories based on the provided query.
     */
    public async getCategoriesAsync(query: Query<PromptCategoryFilter>) {
        const  endpointUrl = this.requestFormatterService.addQueryParams('api/prompts/categories', query);

        return await this.client.getAsync<Array<AnalysisPromptCategory>>(endpointUrl)
    }

    /**
     * Asynchronously retrieves a prompt category by its ID.
     */
    public async  getCategoryByIdAsync(categoryId: string) {
        const  endpointUrl = `api/prompts/categories/${categoryId}`;

        return await this.client.getAsync<AnalysisPromptCategory>(endpointUrl);
    }

    /**
     * Asynchronously retrieves prompt execution results by category ID.
     */
    public async getPromptResultsByCategoryIdAsync(categoryId: string) {
        const  endpointUrl = `api/prompts/categories/${categoryId}/results/`;
        return await  this.client.getAsync<Array<PromptExecutionResult>>(endpointUrl);
    }

    /**
     * Asynchronously retrieves prompt execution results by prompt ID
     */
    public async getPromptResultsByPromptIdAsync(promptId: string) {
        const  endpointUrl =`api/prompts${promptId}/results`;
        return await this.client.getAsync<Array<PromptExecutionResult>>(endpointUrl);
    }

    /**
    * Creates a prompt
   */
    public  async createAsync(command: CreatePromptCommand){
        const endpointUrl = `api/prompts`;
        return this.client.postAsync<AnalysisPrompt>(endpointUrl, command);
    }

    /**
     * Asynchronously updates a prompt based on the provided command.
     */
    public  async updateAsync(command: CreatePromptCommand){
        const endpointUrl = `api/prompts`;
        return await  this.client.putAsync<AnalysisPrompt>(endpointUrl, command);
    }

    /**
     * Asynchronously updates a prompt using the provided command.
     */
    public async updateSelectedPromptAsync(promptCategoryId: string, promptId: string) {
        const  endpointUrl = `api/prompts/categories/${promptCategoryId}/selected/${promptId}`;
        return this.client.putAsync<boolean>(endpointUrl);
    }
}