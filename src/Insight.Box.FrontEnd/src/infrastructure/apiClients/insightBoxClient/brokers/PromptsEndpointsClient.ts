import type ApiClientBase from "@/infrastructure/apiClients/apiClientBase/ApiClientBase";
import type {Query} from "@/infrastructure/models/query/Query";
import type {RequestFormatterService} from "@/infrastructure/apiClients/formatters/RequestFormatterService";
import type {PromptFilter} from "@/modules/prompts/models/PromptFilter";
import type {CreatePromptCommand} from "@/modules/prompts/models/CreatePromptCommand";
import type {FeedbackAnalysisPrompt} from "@/modules/prompts/models/AnalysisPrompt";

export class PromptsEndpointsClient {
    public client: ApiClientBase;
    public requestFormatterService: RequestFormatterService;

    constructor(client: ApiClientBase, requestFormatterService: RequestFormatterService) {
        this.client = client;
        this.requestFormatterService = requestFormatterService;
    }

    public async getAsync(query: Query<PromptFilter>) {
        const endpointUrl = this.requestFormatterService.addQueryParams('api/prompts', query);
        return await this.client.getAsync<Array<FeedbackAnalysisPrompt>>(endpointUrl);
    }

    public async createAsync(command: CreatePromptCommand) {
        const endpointUrl =  'api/prompts';
        return await this.client.postAsync<FeedbackAnalysisPrompt>(endpointUrl, command);
    }
}