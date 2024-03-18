import type {IDropDownValue} from "@/common/components/formDropDown/IDropDownValue";
import type {DropDownValue} from "@/common/components/formDropDown/DropDownValue";
import {WorkflowType} from "@/modules/workflows/models/WorkflowType";

/**
 * Represents a data transfer object (DTO) for a feedback execution workflow.
 */
export class FeedbackAnalysisWorkflow implements IDropDownValue<string, FeedbackAnalysisWorkflow> {
    /*
     * Entity Id
     */
    public id: string;

    /**
     * The name of the feedback execution workflow.
     */
    public name: string;

    /**
     * The unique identifier of the product associated with the workflow.
     */
    public productId: string; // Use 'string' for GUIDs in TypeScript

    /*
     * Product name
     */
    public productName: string; // Use 'string' for GUIDs in TypeScript

    /**
     * The type of the feedback execution workflow.
     */
    public type: WorkflowType;

    constructor() {
        this.name = '';
        this.type = WorkflowType.Training;
    }

    public toDropDownValue(): DropDownValue<string, FeedbackAnalysisWorkflow>{
        return new DropDownValue(this.id, this);
    }
}
