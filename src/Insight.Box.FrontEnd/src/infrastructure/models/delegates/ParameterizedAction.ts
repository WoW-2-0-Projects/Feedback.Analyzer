/*
 * Represents a parameterized abstract callback
 */
export class ParameterizedAction<TArgs> {

    /*
     * Action callback
     */
    public callback: (args: TArgs) => void;

    constructor(callback: (args: TArgs) => void) {
        this.callback = callback;
    }
}