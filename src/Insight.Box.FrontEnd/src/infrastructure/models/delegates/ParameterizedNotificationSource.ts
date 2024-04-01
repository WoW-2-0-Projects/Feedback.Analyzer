/*
 * Represents notification source
 */
import {ParameterizedAction} from "@/infrastructure/models/delegates/ParameterizedAction";

export class ParameterizedNotificationSource<TArgs> {

    private listeners: ParameterizedAction<TArgs>[] = [];

    /*
     * Updates listeners about notification
     */
    public updateListeners = (args: TArgs) => {
        this.listeners.forEach((listener: ParameterizedAction<TArgs>) => {

            listener.callback(args);
        });
    }

    /*
     * Adds listener to the source
     */
    public addListener = (callback: (args: TArgs) => void) => {
        this.listeners.push(new ParameterizedAction<TArgs>(callback));
    }
}