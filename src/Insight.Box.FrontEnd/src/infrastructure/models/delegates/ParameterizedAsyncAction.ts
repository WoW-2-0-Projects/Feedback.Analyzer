/*
 * Represents parameterized async function
 */
export class ParameterizedAsyncAction<TArgs> {

    /*
     * Function callback
     */
    public callback: (args: TArgs) => Promise<void>;

    constructor(callback: (args: TArgs) => Promise<void>) {
        this.callback = callback;
    }

    /*
     * Sets callback
     */
    public setCallback(callback: (args: TArgs) => Promise<void>) {
        this.callback = callback;
    }
}