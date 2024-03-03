/*
 * Represents a product in the organization
 */
export class Prompt {
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
    public version: number;

    /*
     * Indicates whether the product is published
     */
    public isPublished: boolean;

    constructor(id: string, name: string, description: string, isPublished: number) {
        this.id = id;
        this.name = name;
        this.description = description;
        this.isPublished = isPublished;
    }
}