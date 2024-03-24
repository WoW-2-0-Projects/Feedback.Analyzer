import {FeedbackRelevance} from "@/modules/feedbackAnalysisResults/models/FeedbackRelevance";
import {FeedbackOpinion} from "@/modules/feedbackAnalysisResults/models/FeedbackOpinion";
import {FeedbackActionablePoints} from "@/modules/feedbackAnalysisResults/models/FeedbackActionablePoints";
import {FeedbackEntities} from "@/modules/feedbackAnalysisResults/models/FeedbackEntities";
import {FeedbackMetrics} from "@/modules/feedbackAnalysisResults/models/FeedbackMetrics";
import {CustomerFeedback} from "@/modules/feedbackAnalysisResults/models/CustomerFeedback";
import {plainToClass} from "class-transformer";

/**
 * Represents the DTO (Data Transfer Object) for feedback analysis results.
 */
export class FeedbackAnalysisResult {
    /**
     * The unique identifier for the feedback analysis result.
     */
    id: string;

    /**
     * The relevance of the feedback.
     */
    public feedbackRelevance: FeedbackRelevance;

    /**
     * The opinion conveyed by the feedback.
     */
    public feedbackOpinion: FeedbackOpinion;

    /**
     * The actionable points extracted from the feedback.
     */
    public feedbackActionablePoints: FeedbackActionablePoints;

    /**
     * The entities mentioned in the feedback.
     */
    public feedbackEntities: FeedbackEntities;

    /**
     * The metrics associated with the feedback.
     */
    public feedbackMetrics: FeedbackMetrics;

    /**
     * The customer feedback associated with this analysis result.
     */
    public customerFeedback: CustomerFeedback;

    public map() {
        this.feedbackRelevance = plainToClass(FeedbackRelevance, this.feedbackRelevance);
        this.feedbackOpinion = plainToClass(FeedbackOpinion, this.feedbackOpinion);
        this.feedbackActionablePoints = plainToClass(FeedbackActionablePoints, this.feedbackActionablePoints);
        this.feedbackEntities = plainToClass(FeedbackEntities, this.feedbackEntities);
        this.feedbackMetrics = plainToClass(FeedbackMetrics, this.feedbackMetrics);
        this.customerFeedback = plainToClass(CustomerFeedback, this.customerFeedback);
    }
}






