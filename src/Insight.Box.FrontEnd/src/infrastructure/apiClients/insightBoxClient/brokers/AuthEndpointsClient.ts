import type ApiClientBase from "@/infrastructure/apiClients/apiClientBase/ApiClientBase";
import type {SignInDetails} from "@/modules/accounts/models/SignInDetails";
import {IdentityToken} from "@/modules/accounts/models/IdentityToken";
import {Client} from "@/modules/accounts/models/Client";
import type {SignUpDetails} from "@/modules/accounts/models/SignUpDetails";
import {plainToClass} from "class-transformer";
import type {AxiosRequestConfig} from "axios";

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
     * Asynchronously signs up a new user with the provided sign-up details by sending a POST request to the authentication API endpoint.
     */
    public async signUpAsync(signUpDetails: SignUpDetails) {
        const endpointUrl = "api/auth/sign-Up";
        return await this.client.postAsync<IdentityToken>(endpointUrl, signUpDetails);
    }

    /**
     * Retrieves information about the current user.
     * @returns A promise that resolves with the details of the current user.
     */
    public async getCurrentUser() {
        const config: AxiosRequestConfig | any = {mapper: (r: Client) => plainToClass(Client, r)};
        const endpointUrl = 'api/auth/me';
        return await this.client.getAsync<Client>(endpointUrl, config);
    }
}