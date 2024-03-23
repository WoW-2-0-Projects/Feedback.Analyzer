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
} from "@/modules/feedbackAnalysisResults/models/FeedbackAnalysisResultFilter";
import type {AxiosRequestConfig} from "axios";
import {Client} from "@/modules/accounts/models/Client";
import {plainToClass} from "class-transformer";
import {FeedbackAnalysisResult} from "@/modules/feedbackAnalysisResults/models/FeedbackAnalysisResult";

export class FeedbackAnalysisResultsEndpointsClient {
    public client: ApiClientBase;
    public requestFormatterService: RequestFormatterService;

    constructor(client: ApiClientBase, requestFormatterService: RequestFormatterService) {
        this.client = client;
        this.requestFormatterService = requestFormatterService;
    }

    public async getAsync(query: Query<FeedbackAnalysisResultFilter>) {
        const config: AxiosRequestConfig = {mapper: (r: FeedbackAnalysisResult) =>
              plainToClass(FeedbackAnalysisResult, r)}
        const endpointUrl = this.requestFormatterService
            .addQueryParams('api/results', query);
        return await this.client.getAsync<Array<FeedbackAnalysisResult>>(endpointUrl, config);
    }

    public async getByIdAsync(resultId: string) {
        const config: AxiosRequestConfig = {mapper: (r: FeedbackAnalysisResult) =>
              plainToClass(FeedbackAnalysisResult, r)}
        const endpointUrl = `api/results/${resultId}`;
        return await this.client.getAsync<Array<FeedbackAnalysisResult>>(endpointUrl, config);
    }
}