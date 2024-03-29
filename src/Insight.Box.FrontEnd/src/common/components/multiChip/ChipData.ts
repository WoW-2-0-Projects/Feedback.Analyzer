/*
 * Represents chip data
 */
import type {ActionType} from "@/common/components/actions/ActionType";

export class ChipData {
    /*
     * Chip text
     */
    public text: string;

    /*
     * Determines whether to show border
     */
    public showBorder: boolean;

    /*
     * Determines chip type
     */
    public type: ActionType;

    constructor(text: string, type: ActionType, showBorder: boolean = false) {
        this.text = text;
        this.type = type;
        this.showBorder = showBorder;
    }
}