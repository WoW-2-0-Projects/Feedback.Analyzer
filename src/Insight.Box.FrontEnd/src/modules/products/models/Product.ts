/*
 * Represents a product in the organization
 */
export class Product {
    /*
     * Product id
     */
    public id: string;

    /*
     * Product name
     */
    public name: string;

    /*
     * Product description
     */
    public description: string;

    /*
     * Product organization id
     */
    public organizationId: string;

    constructor(id: string, name: string, description: string, organizationId: number) {
        this.id = id ?? '';
        this.name = name ?? '';
        this.description = description ?? '';
        this.organizationId = organizationId ?? '';
    }
}