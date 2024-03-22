import {Command} from "@/infrastructure/models/command/Command";
import type {Product} from "@/modules/products/models/Product";

export class  UpdateProductCommand extends Command{

    public product:Product;

    public constructor(product: Product) {
        super();
        this.product = product;
    }

}