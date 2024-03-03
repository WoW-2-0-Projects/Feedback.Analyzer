import {Command} from "@/infrastructure/models/command/Command";
import type {Product} from "@/modules/products/models/Product";

/*
 * Represents create product model
 */
export class CreateProductCommand extends Command {

    /*
     * Product to be created
     */
    public product: Product;

    constructor(product: Product) {
        super();

        this.product = product;
    }
}