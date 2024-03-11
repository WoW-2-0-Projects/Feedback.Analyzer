/*
 * Represents an abstract callback
 */
export class Action {

    constructor(callback: () => void) {
        this.callBack = callback;
    }

    /*
     * Action callback
     */
    public callBack: () => void;
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

            listener.callBack();
        });
    }

    /*
     * Adds listener to the source
     */
    public addListener = (callback: () => void) => {
        this.listeners.push(new Action(callback));
    }
}