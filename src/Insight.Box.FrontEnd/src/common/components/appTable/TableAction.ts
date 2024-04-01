import type {ParameterizedAction} from "@/infrastructure/models/delegates/ParameterizedAction";
import type {ParameterizedAsyncAction} from "@/infrastructure/models/delegates/ParameterizedAsyncAction";
import type {Action} from "@/infrastructure/models/delegates/Action";
import type {ActionType} from "@/common/components/actions/ActionType";
import type {AsyncFunction} from "@/infrastructure/models/delegates/AsyncFunction";

export class TableAction {
    public action: Action | AsyncFunction | ParameterizedAction<any> | ParameterizedAsyncAction<any>;
    public icon: string;
    public buttonType: ActionType;

    constructor(action: Action | AsyncFunction | ParameterizedAction<any> | ParameterizedAsyncAction<any>, actionType: ActionType, icon: string) {
        this.action = action;
        this.buttonType = actionType;
        this.icon = icon;
    }
}