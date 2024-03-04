/*
 * Represents an organization
 */
export class Organization {
    /*
     * Organization id
     */
    public id: string;

    /*
     * Client id of the organization
     */
    public clientId: string;

    /*
     * Name of the organization
     */
    public name: string = '';

    /*
     * Description of the organization
     */
    public description: string = '';

    /*
     * Count of products in the organization
     */
    public productsCount: number;
}