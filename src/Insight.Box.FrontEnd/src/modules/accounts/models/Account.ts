import type {Client} from "@/modules/accounts/models/Client";

/**
 * Represents an account associated with a client.
 * It provides functionality to check if a client is logged in.
 */
export class Account {

    /**
     * The client associated with this account.
     */
    public client!: Client;

    /**
     * Checks if the client is logged in.
     * @returns A boolean indicating whether the client is logged in or not.
     */
    public isLoggedIn = () =>
        // Return true if the client is not null or undefined, indicating that they are logged in
        this.client !== null && this.client !== undefined;
}
