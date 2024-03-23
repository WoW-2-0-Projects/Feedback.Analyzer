import type {Action} from "@/infrastructure/models/delegates/Action";
import type {ActionType} from "@/common/components/actions/ActionType";

export class TableAction {
    public action: Action;
    public icon: string;
    public buttonType: ActionType;

    constructor(action: Action, actionType: ActionType, icon: string) {
        this.action = action;
        this.buttonType = actionType;
        this.icon = icon;
    }
}