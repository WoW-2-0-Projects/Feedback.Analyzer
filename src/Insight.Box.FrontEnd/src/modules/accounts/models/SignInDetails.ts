

/**
 * Represents the details required for user sign-in.
 */
export class SignInDetails {
    /**
     * The email address of the user.
     */
    public emailAddress: string = '';

    /**
     * The password associated with the user's account.
     */
    public password: string = '';

    /**
     * Constructs a new instance of SignInDetails class.
     * Initializes email address and password to empty strings by default.
     */
    constructor() {
        this.emailAddress = "";
        this.password = "";
    }
}
