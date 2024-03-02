/*
 * Represents generic command model
 */
export class Command<TModel extends object> {
    /*
     * Command model
     */
    public model: TModel | null;

    constructor(model: TModel | null = null) {
        this.model = model as TModel;
    }
}