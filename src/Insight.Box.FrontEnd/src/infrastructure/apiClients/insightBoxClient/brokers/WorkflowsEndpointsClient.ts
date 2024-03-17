import type ApiClientBase from "@/infrastructure/apiClients/apiClientBase/ApiClientBase";
import type {Query} from "@/infrastructure/models/query/Query";
import type {RequestFormatterService} from "@/infrastructure/apiClients/formatters/RequestFormatterService";
import type {FeedbackAnalysisWorkflow} from "@/modules/prompts/models/FeedbackAnalysisWorkflow";
import type {FeedbackAnalysisWorkflowFilter} from "@/modules/workflows/models/FeedbackAnalysisWorkflowFilter";
import type {
    CreateFeedbackAnalysisWorkflowCommand
} from "@/modules/workflows/models/CreateFeedbackAnalysisWorkflowCommand";
import type {
    UpdateFeedbackAnalysisWorkflowCommand
} from "@/modules/workflows/models/UpdateFeedbackAnalysisWorkflowCommand";

export class WorkflowsEndpointsClient {
    public client: ApiClientBase;
    public requestFormatterService: RequestFormatterService;

    constructor(client: ApiClientBase, requestFormatterService: RequestFormatterService) {
        this.client = client;
        this.requestFormatterService = requestFormatterService;
    }

    public async getAsync(query: Query<FeedbackAnalysisWorkflowFilter>) {
        const endpointUrl = this.requestFormatterService.addQueryParams('api/workflows', query);
        return await this.client.getAsync<Array<FeedbackAnalysisWorkflow>>(endpointUrl);
    }

    public async createAsync(command: CreateFeedbackAnalysisWorkflowCommand) {
        const endpointUrl =  'api/workflows';
        return await this.client.postAsync<FeedbackAnalysisWorkflow>(endpointUrl, command);
    }

    public async updateAsync(command: UpdateFeedbackAnalysisWorkflowCommand) {
        const endpointUrl =  'api/workflows';
        return await this.client.putAsync<FeedbackAnalysisWorkflow>(endpointUrl, command);
    }

    public async executeSinglePromptAsync(workflowId: string, promptId: string) {
        const endpointUrl = `api/workflows/${workflowId}/execute/${promptId}`;
        return await this.client.postAsync(endpointUrl);
    }
}