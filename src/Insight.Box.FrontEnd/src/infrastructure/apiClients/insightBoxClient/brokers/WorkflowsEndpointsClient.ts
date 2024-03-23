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
import type {
    FeedbackAnalysisWorkflowResultFilter
} from "@/modules/workflows/models/FeedbackAnalysisWorkflowResultFilter";
import {FeedbackAnalysisWorkflowResult} from "@/modules/workflows/models/FeedbackAnalysisWorkflowResult";
import {
    FeedbackAnalysisResultFilter
} from "@/infrastructure/apiClients/insightBoxClient/brokers/FeedbackAnalysisResultFilter";
import type {AxiosRequestConfig} from "axios";
import {Client} from "@/modules/accounts/models/Client";
import {plainToClass} from "class-transformer";
import {FeedbackAnalysisResult} from "@/modules/feedbackAnalysisResults/models/FeedbackAnalysisResult";

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

    public async getResultsAsync(query: Query<FeedbackAnalysisWorkflowResultFilter>) {
        const endpointUrl = this.requestFormatterService.addQueryParams(`api/workflows/${query.filter.workflowId}/results`, query);
        return await this.client.getAsync<Array<FeedbackAnalysisWorkflowResult>>(endpointUrl);
    }

    public async getFeedbackResultsAsync(query: Query<FeedbackAnalysisResultFilter>) {
        const config: AxiosRequestConfig = {mapper: (r: FeedbackAnalysisResult) =>
              plainToClass(FeedbackAnalysisResult, r)}
        const endpointUrl = this.requestFormatterService
          .addQueryParams(`api/workflows/${query.filter.workflowId}/results/${query.filter.resultId}/results`, query);
        return await this.client.getAsync<Array<FeedbackAnalysisResult>>(endpointUrl, config);
    }

    public async createAsync(command: CreateFeedbackAnalysisWorkflowCommand) {
        const endpointUrl = 'api/workflows';
        return await this.client.postAsync<FeedbackAnalysisWorkflow>(endpointUrl, command);
    }

    public async updateAsync(command: UpdateFeedbackAnalysisWorkflowCommand) {
        const endpointUrl = 'api/workflows';
        return await this.client.putAsync<FeedbackAnalysisWorkflow>(endpointUrl, command);
    }

    public async deleteByIdAsync(workflowId: string) {
        const endpointUrl = `api/workflows/${workflowId}`;
        return await this.client.deleteAsync(endpointUrl);
    }

    public async executeSinglePromptAsync(workflowId: string, promptId: string) {
        const endpointUrl = `api/workflows/${workflowId}/execute/${promptId}`;
        return await this.client.postAsync(endpointUrl);
    }
}