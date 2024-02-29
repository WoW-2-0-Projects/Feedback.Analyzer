import  ApiClientBase from "@/infrastructure/apiClients/apiClientBase/ApiClientBase";

export class InsightBoxApiClient {
    private readonly client: ApiClientBase;
    public readonly baseUrl: string;

    constructor() {
        this.baseUrl = "https://localhost:7266";

        this.client = new ApiClientBase({
            baseURL: this.baseUrl,
            withCredentials: true
        })

        }
}