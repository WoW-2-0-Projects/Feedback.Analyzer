
/**
 * Represents an identity token containing access and refresh tokens.
 */
export class IdentityToken {

    /**
     * The access token used for authentication and authorization.
     */
    public accessToken!: string;

    /**
     * The refresh token used for obtaining a new access token after expiration.
     */
    public refreshToken!: string;
}

