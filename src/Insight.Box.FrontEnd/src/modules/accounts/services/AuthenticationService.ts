import {InsightBoxApiClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/InsightBoxApiClient";
import {LocalStorageService} from "@/infrastructure/services/storage/LocalStorageService";
import {useAccountStore} from "@/infrastructure/stores/AccountStore";
import {Account} from "@/modules/accounts/models/Account";
import type {SignInDetails} from "@/modules/accounts/models/SignInDetails";
import {Client} from "@/modules/accounts/models/Client";

export class AuthenticationService {
    private insightBoxApiClient: InsightBoxApiClient;
    private localStorageService: LocalStorageService;
    private accountStore = useAccountStore()

    constructor() {
        this.insightBoxApiClient = new InsightBoxApiClient();
        this.localStorageService = new LocalStorageService();
    }

    public hasAccessToken() {
        return this.localStorageService.get('accessToken') !== null;
    }

    public  isLoggedIn() {
        return this.accountStore.account.isLoggedIn();
    }

    public  async signInAsync(signInDetails: SignInDetails) {
        const signInResponse = await this.insightBoxApiClient.auth.signInAsync(signInDetails);

        if(!signInResponse.isSuccess)
            return false;

        // Store security tokens
        this.localStorageService.Set('accessToken', signInResponse.response?.accessToken)
        this.localStorageService.Set('refreshToken', signInResponse.response?.refreshToken)

        await this.setCurrentUser()

        return true;
    }


    public async setCurrentUser() {
        if(this.isLoggedIn() || !this.hasAccessToken()) return

        const currentUser = await this.insightBoxApiClient.auth.getCurrentUser();
        if(currentUser.isSuccess){
            const account = new Account();
            account.client = currentUser.response!;
            this.accountStore.set(account);
        }
    }
}