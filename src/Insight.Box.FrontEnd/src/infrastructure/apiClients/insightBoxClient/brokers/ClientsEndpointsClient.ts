import type ApiClientBase from "@/infrastructure/apiClients/apiClientBase/ApiClientBase";

/**
 * Represents a client for interacting with client-related endpoints.
 */
export class ClientsEndpointsClient {

    /**
     * The underlying API client used to make requests.
     */
    public client: ApiClientBase;

    /**
     * Constructs a new instance of ClientsEndpointsClient.
     * @param client The API client to use for making requests.
     */
    constructor(client: ApiClientBase) {
        this.client = client;
    }

    /**
     * Retrieves client information by email address.
     * @param emailAddress The email address of the client to retrieve information for.
     * @returns A promise that resolves with client details in string format.
     */
    public async checkByEmailAsync(emailAddress: string) {
        const endpointUrl = `api/clients/by-email/${emailAddress}`;
        return await this.client.getAsync<string>(endpointUrl);
    }
}