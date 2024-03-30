import type {ParameterizedAction} from "@/infrastructure/models/delegates/ParameterizedAction";
import type {ParameterizedAsyncAction} from "@/infrastructure/models/delegates/ParameterizedAsyncAction";
import type {Action} from "@/infrastructure/models/delegates/Action";
import type {ActionType} from "@/common/components/actions/ActionType";

export class TableAction {
    public action: Action | ParameterizedAction<any> | ParameterizedAsyncAction<any>;
    public icon: string;
    public buttonType: ActionType;

    constructor(action: Action | ParameterizedAction<any> | ParameterizedAsyncAction<any>, actionType: ActionType, icon: string) {
        this.action = action;
        this.buttonType = actionType;
        this.icon = icon;
    }
}