import type ApiClientBase from "@/infrastructure/apiClients/apiClientBase/ApiClientBase";
import type {Query} from "@/infrastructure/models/query/Query";
import type {RequestFormatterService} from "@/infrastructure/apiClients/formatters/RequestFormatterService";
import type {FeedbackExecutionWorkflowFilter} from "@/modules/prompts/models/FeedbackExecutionWorkflowFilter";
import type {FeedbackExecutionWorkflow} from "@/modules/prompts/models/FeedbackExecutionWorkflow";

export class WorkflowsEndpointsClient {
    public client: ApiClientBase;
    public requestFormatterService: RequestFormatterService;

    constructor(client: ApiClientBase, requestFormatterService: RequestFormatterService) {
        this.client = client;
        this.requestFormatterService = requestFormatterService;
    }

    public async getAsync(query: Query<FeedbackExecutionWorkflowFilter>) {
        const endpointUrl = this.requestFormatterService.addQueryParams('api/workflows', query);
        return await this.client.getAsync<Array<FeedbackExecutionWorkflow>>(endpointUrl);
    }

    public async executeSinglePrompt(workflowId: string, promptId: string) {
        const endpointUrl = `api/workflows/${workflowId}/execute/${promptId}`;
        return await this.client.postAsync(endpointUrl);
    }

}