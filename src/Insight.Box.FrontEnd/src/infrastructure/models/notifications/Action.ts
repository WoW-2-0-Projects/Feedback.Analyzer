/*
 * Represents an abstract callback
 */
export class Action<TArgs> {

    /*
     * Action callback
     */
    public callBack: () => void;

    constructor(callback: () => void) {
        this.callBack = callback;
    }


    /*
     * Sets callback
    */
    public setCallback(callback: (args: TArgs) => void) {
        this.callBack = callback;
    }
}

/*
 * Represents notification source
 */
export class NotificationSource<TArgs> {

    private listeners: Action<TArgs>[] = [];

    /*
     * Updates listeners about notification
     */
    public updateListeners = (args: TArgs) => {
        this.listeners.forEach((listener: Action<TArgs>) => {

            listener.callBack(args);
        });
    }

    /*
     * Adds listener to the source
     */
    public addListener = (callback: (args: TArgs) => void) => {
        this.listeners.push(new Action<TArgs>(callback));
    }
}