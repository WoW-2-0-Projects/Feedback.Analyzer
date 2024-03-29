import type {IDropDownValue} from "@/common/components/formDropDown/IDropDownValue";
import {DropDownValue} from "@/common/components/formDropDown/DropDownValue";
import type {WorkflowType} from "@/modules/workflows/models/WorkflowType";

/**
 * Represents a feedback analysis workflow.
 */
export class FeedbackAnalysisWorkflow implements IDropDownValue<string, FeedbackAnalysisWorkflow> {

    /**
    * Entity id
    */
    public id!: string;

    /**
     * The name of the feedback execution workflow.
     */
    public name!: string;

    /**
     * The unique identifier of the product associated with the workflow.
     */
    public productId!: string; // Use 'string' for GUIDs in TypeScript

    /**
     * The type of the feedback execution workflow.
     */
    public type!: WorkflowType;

    /**
     * Indicates whether all prompts are set for the workflow.
     */
    public allPromptsSet!: boolean;

    public toDropDownValue(): DropDownValue<string, FeedbackAnalysisWorkflow>{
        return new DropDownValue(this.id, this);
    }
}