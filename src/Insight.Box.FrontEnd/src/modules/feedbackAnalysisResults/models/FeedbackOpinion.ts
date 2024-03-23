import {OpinionType} from "@/modules/feedbackAnalysisResults/models/OpinionType";
import {ActionType} from "@/common/components/actions/ActionType";

/**
 * Represents opinions provided in feedback.
 */
export class FeedbackOpinion {
    /**
     * The overall opinion type of the feedback.
     */
    public overallOpinion: OpinionType;

    /**
     * An array of positive points mentioned in the feedback.
     */
    public positiveOpinionPoints: string[];

    /**
     * An array of negative points mentioned in the feedback.
     */
    public negativeOpinionPoints: string[];

    public mapOpinionToActionType(): ActionType {
        switch (this.overallOpinion) {
            case OpinionType.Positive:
                return ActionType.Success;
            case OpinionType.Negative:
                return ActionType.Danger
            case OpinionType.Neutral:
                return ActionType.Secondary;
        }
    }
}