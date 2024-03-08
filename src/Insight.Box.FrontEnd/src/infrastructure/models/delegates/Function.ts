/*
 * Represents function used between components
*/
export class Function<TArgs> {

    /*
     * Function callback
     */
    public callback: (args: TArgs) => void;

    constructor(callback: (args: TArgs) => void) {
        this.callback = callback;
    }

    /*
     * Sets callback
     */
    public setCallback(callback: (args: TArgs) => void) {
        this.callback = callback;
    }
}

/* Represents async function used between components */
export class AsyncFunction<TArgs> {

    /*
     * Function callback
     */
    public callBack: (args: TArgs) => Promise;

    constructor(callback: (args: TArgs) => Promise) {
        this.callBack = callback;
    }

    /*
     * Sets callback
     */
    public setCallback(callback: (args: TArgs) => Promise) {
        this.callBack = callback;
    }
}