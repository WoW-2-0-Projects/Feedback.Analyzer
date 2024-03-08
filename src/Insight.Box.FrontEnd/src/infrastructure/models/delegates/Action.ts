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

    /*
     * Sets callback
     */
    public setCallback(callback: (args: TArgs) => void) {
        this.callback = callback;
    }
}

/*
 * Represents notification source
 */
export class NotificationSource {

    private listeners: Action[] = [];

    /*
     * Updates listeners about notification
     */
    public updateListeners = () => {
        this.listeners.forEach((listener: Action) => {

            listener.callback();
        });
    }

    /*
     * Adds listener to the source
     */
    public addListener = (callback: () => void) => {
        this.listeners.push(new Action(callback));
    }
}