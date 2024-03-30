/*
 * Represents an abstract callback
 */
export class Action {

    /*
     * Action callback
     */
    public callback: () => void;

    constructor(callback: () => void) {
        this.callback = callback;
    }
}