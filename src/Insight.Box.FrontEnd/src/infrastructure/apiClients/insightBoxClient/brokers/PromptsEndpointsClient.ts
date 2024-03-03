import type ApiClientBase from "@/infrastructure/apiClients/apiClientBase/ApiClientBase";
import type {Query} from "@/infrastructure/models/query/Query";
import type {RequestFormatterService} from "@/infrastructure/apiClients/formatters/RequestFormatterService";
import type {PromptFilter} from "@/modules/prompts/models/PromptFilter";
import type {CreatePromptCommand} from "@/modules/prompts/models/CreatePromptCommand";
import type {AnalysisPrompt} from "@/modules/prompts/models/AnalysisPrompt";
import {PromptCategoryFilter} from "@/modules/prompts/models/PromptCategoryFilter";
import type {AnalysisPromptCategory} from "@/modules/prompts/models/AnalysisPromptCategory";

export class PromptsEndpointsClient {
    public client: ApiClientBase;
    public requestFormatterService: RequestFormatterService;

    constructor(client: ApiClientBase, requestFormatterService: RequestFormatterService) {
        this.client = client;
        this.requestFormatterService = requestFormatterService;
    }

    public async getAsync(query: Query<PromptFilter>) {
        const endpointUrl = this.requestFormatterService.addQueryParams('api/prompts', query);
        return await this.client.getAsync<Array<AnalysisPrompt>>(endpointUrl);
    }

    public async getCategoriesAsync(query: Query<PromptCategoryFilter>) {
        const endpointUrl = this.requestFormatterService.addQueryParams('api/prompts/categories', query);
        return await this.client.getAsync<Array<AnalysisPromptCategory>>(endpointUrl);
    }

    public async createAsync(command: CreatePromptCommand) {
        const endpointUrl =  'api/prompts';
        return await this.client.postAsync<AnalysisPrompt>(endpointUrl, command);
    }
}