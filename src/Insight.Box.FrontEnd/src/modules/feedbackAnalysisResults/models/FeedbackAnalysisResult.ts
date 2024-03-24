import {CustomerFeedback} from "@/modules/feedbackAnalysisResults/models/CustomerFeedback";
import {plainToClass} from "class-transformer";
import {WorkflowStatus} from "@/modules/workflows/models/WorkflowStatus";
import {OpinionType} from "@/modules/feedbackAnalysisResults/models/OpinionType";
import {ActionType} from "@/common/components/actions/ActionType";

/**
 * Represents the DTO (Data Transfer Object) for feedback analysis results.
 */
export class FeedbackAnalysisResult {
    /**
     * The unique identifier for the feedback analysis result.
     */
    public id: string;

    /**
     * A value indicating whether the feedback is relevant.
     */
    public isRelevant: boolean;

    /**
     * The overall opinion type of the feedback.
     */
    public opinion: OpinionType;

    /**
     * An array of recognized languages in the feedback content.
     */
    public languages: string[];

    /**
     * The status of the analysis.
     */
    public status: WorkflowStatus;

    /**
     * An array of positive points mentioned in the feedback.
     */
    public positiveOpinionPoints: string[];

    /**
     * An array of negative points mentioned in the feedback.
     */
    public negativeOpinionPoints: string[];

    /*
     * Customer ideas about the product.
     */
    public ideas: string[];

    /*
     * Customer questions about the product.
     */
    public questions: string[];

    /*
     * The time spend to execute the semantic analysis model
     */
    public modelExecutionDurationInMilliseconds: number;

    /**
     * The time taken to analyze the feedback in milliseconds.
     */
    public analysisDurationInMilliseconds: number;

    /**
     * The customer feedback associated with this analysis result.
     */
    public customerFeedback: CustomerFeedback;

    public map() {
        this.customerFeedback = plainToClass(CustomerFeedback, this.customerFeedback);
    }

    public mapOpinionToActionType(): ActionType {
        switch (this.opinion) {
            case OpinionType.Positive:
                return ActionType.Success;
            case OpinionType.Negative:
                return ActionType.Danger
            case OpinionType.Neutral:
                return ActionType.Secondary;
        }
    }

    public mapStatusToActionType() {
        switch(this.status)
        {
            case WorkflowStatus.Queued : return ActionType.Secondary;
            case WorkflowStatus.Failed : return ActionType.Danger;
            case WorkflowStatus.Running : return ActionType.Secondary;
            case WorkflowStatus.Completed : return ActionType.Success;
        }
    }
}






