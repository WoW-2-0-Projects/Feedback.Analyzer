import {Expose} from "class-transformer";

/**
 * Represents a client entity with basic information.
 */
export class Client {

    /*
     * Entity Id
     */
    public id!: string;

    /**
     * The first name of the client.
     */
    public firstName!: string;

    /**
     * The last name of the client.
     */
    public lastName!: string;

    /**
     * The email address of the client.
     */
    public emailAddress!: string;
}
