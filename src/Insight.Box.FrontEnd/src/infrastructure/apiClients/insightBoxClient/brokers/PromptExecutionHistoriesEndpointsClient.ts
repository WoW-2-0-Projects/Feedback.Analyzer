import type ApiClientBase from "@/infrastructure/apiClients/apiClientBase/ApiClientBase";
import type {RequestFormatterService} from "@/infrastructure/apiClients/formatters/RequestFormatterService";
import type {PromptsExecutionHistory} from "@/modules/prompts/models/PromptExecutionHistory";

export class PromptExecutionHistoriesEndpointsClient {
    public client: ApiClientBase;
    public requestFormatterService: RequestFormatterService;

    constructor(client: ApiClientBase, requestFormatterService: RequestFormatterService) {
        this.client = client;
        this.requestFormatterService = requestFormatterService;
    }

    public async getByPromptIdAsync(promptId: string) {
        const endpointUrl = `api/prompts/${promptId}/histories`;
        return await this.client.getAsync<Array<PromptsExecutionHistory>>(endpointUrl);
    }
}