/*
 * Represents notification source
 */
import {Action} from "@/infrastructure/models/delegates/Action";
import {AsyncFunction} from "@/infrastructure/models/delegates/AsyncFunction";

export class NotificationSource {

    private listeners: Action[] = [];
    private asynclisteners: AsyncFunction[] = [];

    /*
     * Updates listeners about notification
     */
    public updateListenersAsync = async () => {
        this.listeners.forEach((listener: Action) => listener.callback());
        this.asynclisteners.forEach(async (listener: AsyncFunction) => await listener.callback());
    }


    /*
     * Adds listener to the source
     */
    public addListener(callback: () => void): void {
        this.listeners.push(new Action(callback));
    }

    /*
     * Adds an asynchronous listener to the source
     */
    public addAsyncListener(callback: () => Promise<void>): void {
        this.asynclisteners.push(new AsyncFunction(callback));
    }
}