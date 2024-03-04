import type {Action} from "@/infrastructure/models/notifications/Action";
import type {ButtonType} from "@/common/components/appButton/ButtonType";

export class TableAction {
    public action: Action;
    public icon: string;
    public buttonType: ButtonType;

    constructor(action: Action, actionType: ButtonType, icon: string) {
        this.action = action;
        this.buttonType = actionType;
        this.icon = icon;
    }
}