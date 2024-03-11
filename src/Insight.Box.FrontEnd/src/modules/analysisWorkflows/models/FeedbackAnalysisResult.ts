/*
 * Represents the result of the feedback analysis
 */
import {FeedbackRelevance} from "@/modules/analysisWorkflows/models/FeedbackRelevance";

export class FeedbackAnalysisResult{

    /*
        * The id of the feedback analysis result
     */
    public id: string;

    /*
     * Relevance details
     */
    public feedbackRelevance: FeedbackRelevance;
}