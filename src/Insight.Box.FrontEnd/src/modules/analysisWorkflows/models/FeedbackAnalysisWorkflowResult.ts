
/*
 * Represents the result of the feedback analysis workflow
 */
import {FeedbackAnalysisResult} from "@/modules/analysisWorkflows/models/FeedbackAnalysisResult";

export class FeedbackAnalysisWorkflowResult{

    public id: string;

    public workflowId: string;

    public startedTime: Date;

    public finishedTime: Date;

    public results: Array<FeedbackAnalysisResult>;
}