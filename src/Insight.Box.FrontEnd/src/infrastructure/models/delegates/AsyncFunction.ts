/*
 * Represents an abstract callback
 */
export class AsyncFunction {

    /*
     * Action callback
     */
    public callback: () => Promise<void>;

    constructor(callback: () => Promise<void>) {
        this.callback = callback;
    }
}