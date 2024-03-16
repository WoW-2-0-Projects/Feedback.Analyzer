import {InsightBoxApiClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/InsightBoxApiClient";
import {localStorageService} from "@/infrastructure/services/storage/LocalStorageService";
import {useAccountStore} from "@/infrastructure/stores/AccountStore";
import {Account} from "@/modules/accounts/models/Account";
import type {SignInDetails} from "@/modules/accounts/models/SignInDetails";
import type {SignUpDetails} from "@/modules/accounts/models/SignUpDetails";

/**
 * AuthenticationService class handles authentication-related functionalities such as checking access token,
 * user login status, signing in, and setting the current user.
 */
export class AuthenticationService {
    private insightBoxApiClient: InsightBoxApiClient; // Instance of InsightBoxApiClient for API interactions
    private localStorageService: localStorageService; // Instance of LocalStorageService for storing tokens
    private accountStore = useAccountStore(); // Instance of account store for managing user account data

    /**
     * Constructor for AuthenticationService class.
     * Initializes InsightBoxApiClient and LocalStorageService instances.
     */
    constructor() {
        this.insightBoxApiClient = new InsightBoxApiClient();
        this.localStorageService = new localStorageService();
    }

    /**
     * Checks if an access token exists in the local storage.
     * @returns {boolean} True if an access token exists, otherwise false.
     */
    public hasAccessToken() {
        return this.localStorageService.get('accessToken') !== null;
    }

    /**
     * Checks if the user is logged in.
     * @returns {boolean} True if the user is logged in, otherwise false.
     */
    public isLoggedIn() {
        return this.accountStore.account.isLoggedIn();
    }

    /**
     * Attempts to sign in the user asynchronously.
     * @param {SignInDetails} signInDetails - The sign-in details.
     * @returns {Promise<boolean>} A promise that resolves to true if sign-in is successful, otherwise false.
     */
    public async signInAsync(signInDetails: SignInDetails) {
        const signInResponse = await this.insightBoxApiClient.auth.signInAsync(signInDetails);

        if(!signInResponse.isSuccess)
            return false;

        // Store security tokens
        this.localStorageService.Set('accessToken', signInResponse.response?.accessToken);
        this.localStorageService.Set('refreshToken', signInResponse.response?.refreshToken);

        await this.setCurrentUser();

        return true;
    }

    /**
     * Asynchronously signs up a new user with the provided sign-up details.
     */
    public async signUpAsync(signUpDetails: SignUpDetails) {
        const  signUpResponse = await this.insightBoxApiClient.auth.signUpAsync(signUpDetails)

        if(!signUpResponse.isSuccess)
            return false;

        // Store security tokens
        this.localStorageService.Set('accessToken', signUpResponse.response?.accessToken);
        this.localStorageService.Set('refreshToken', signUpResponse.response?.refreshToken);
    }

    /**
     * Sets the current user after successful sign-in.
     */
    public async setCurrentUser() {
        if(this.isLoggedIn() || !this.hasAccessToken()) return;

        const currentUser = await this.insightBoxApiClient.auth.getCurrentUser();
        if(currentUser.isSuccess){
            const account = new Account();
            account.client = currentUser.response!;
            this.accountStore.set(account);
        }
    }
}
