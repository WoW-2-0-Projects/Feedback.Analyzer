import type {WorkflowType} from "@/modules/prompts/models/WorkflowType";
import type {IDropDownValue} from "@/common/components/formDropDown/IDropDownValue";
import type {DropDownValue} from "@/common/components/formDropDown/DropDownValue";

/**
 * Represents a data transfer object (DTO) for a feedback execution workflow.
 */
export class FeedbackExecutionWorkflow implements IDropDownValue<string, FeedbackExecutionWorkflow> {
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

    /**
     * The type of the feedback execution workflow.
     */
    public type: WorkflowType;

    public toDropDownValue(): DropDownValue<string, FeedbackExecutionWorkflow>{
        return new DropDownValue(this.id, this);
    }

}
