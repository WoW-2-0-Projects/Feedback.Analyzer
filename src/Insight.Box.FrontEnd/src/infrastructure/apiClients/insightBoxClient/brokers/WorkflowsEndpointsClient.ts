import type ApiClientBase from "@/infrastructure/apiClients/apiClientBase/ApiClientBase";
import type {Query} from "@/infrastructure/models/query/Query";
import type {RequestFormatterService} from "@/infrastructure/apiClients/formatters/RequestFormatterService";
import type {AnalysisWorkflowFilter} from "@/modules/prompts/models/AnalysisWorkflowFilter";
import type {FeedbackAnalysisWorkflow} from "@/modules/prompts/models/FeedbackAnalysisWorkflow";
import {FeedbackAnalysisWorkflowResult} from "@/modules/analysisWorkflows/models/FeedbackAnalysisWorkflowResult";

export class WorkflowsEndpointsClient {
    public client: ApiClientBase;
    public requestFormatterService: RequestFormatterService;

    constructor(client: ApiClientBase, requestFormatterService: RequestFormatterService) {
        this.client = client;
        this.requestFormatterService = requestFormatterService;
    }

    public async getAsync(query: Query<AnalysisWorkflowFilter>) {
        const endpointUrl = this.requestFormatterService.addQueryParams('api/workflows', query);
        return await this.client.getAsync<Array<FeedbackAnalysisWorkflow>>(endpointUrl);
    }

    public async getResultsById(workflowId: string) {
        const endpointUrl = `api/workflows/${workflowId}/results`;
        return await this.client.getAsync<Array<FeedbackAnalysisWorkflowResult>>(endpointUrl);
    }

    public async executeSinglePromptAsync(workflowId: string, promptId: string) {
        const endpointUrl = `api/workflows/${workflowId}/execute/${promptId}`;
        return await this.client.postAsync(endpointUrl);
    }

    public async executeWorkflowAsync(workflowId: string) {
        const endpointUrl = `api/workflows/${workflowId}/execute`;
        return await this.client.postAsync(endpointUrl);
    }

}