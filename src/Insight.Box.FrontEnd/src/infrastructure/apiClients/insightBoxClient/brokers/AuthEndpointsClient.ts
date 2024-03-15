import type ApiClientBase from "@/infrastructure/apiClients/apiClientBase/ApiClientBase";
import type {SignInDetails} from "@/modules/accounts/models/SignInDetails";
import {IdentityToken} from "@/modules/accounts/models/IdentityToken";
import {Client} from "@/modules/accounts/models/Client";

/**
 * Represents a client for interacting with authentication endpoints.
 */
export class AuthEndpointsClient {

    /**
     * The underlying API client used to make requests.
     */
    public client: ApiClientBase;

    /**
     * Constructs a new instance of AuthEndpointsClient.
     * @param client The API client to use for making requests.
     */
    constructor(client: ApiClientBase) {
        this.client = client;
    }

    /**
     * Sends a sign-in request to the server.
     * @param signInDetails The sign-in details containing user credentials.
     * @returns A promise that resolves with an identity token upon successful sign-in.
     */
    public async signInAsync(signInDetails: SignInDetails) {
        const endpointUrl = "api/auth/sign-in";
        return await this.client.postAsync<IdentityToken>(endpointUrl, signInDetails);
    }

    /**
     * Retrieves information about the current user.
     * @returns A promise that resolves with the details of the current user.
     */
    public async getCurrentUser() {
        const endpointUrl = 'api/auth/me';
        return await this.client.getAsync<Client>(endpointUrl);
    }
}