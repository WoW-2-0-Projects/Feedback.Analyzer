/*
 * Represents a product in the organization
 */
export class Product {
    /*
     * Product id
     */
    public id!: string;

   /*
    * Product organization id
    */
    public organizationId!: string;

    /*
     * Product name
     */
    public name: string = '';

    /*
     * Product description
     */
    public description: string = '';

    constructor() {
        this.name = ' ';
        this.description = ' ';
    }
}