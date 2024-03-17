import {Command} from "@/infrastructure/models/command/Command";
import type {FeedbackAnalysisWorkflow} from "@/modules/workflows/models/FeedbackAnalysisWorkflow";

/*
 * Represents create feedback analysis workflow model
 */
export class CreateFeedbackAnalysisWorkflowCommand extends Command {

    /*
     * Product to be created
     */
    public feedbackAnalysisWorkflow: FeedbackAnalysisWorkflow;

    /*
     * Base workflow id
     */
    public baseWorkflowId: string;

    constructor(feedbackAnalysisWorkflow: FeedbackAnalysisWorkflow, baseWorkflowId: string) {
        super();

        this.feedbackAnalysisWorkflow = feedbackAnalysisWorkflow;
        this.baseWorkflowId = baseWorkflowId;
    }
}