import {Command} from "@/infrastructure/models/command/Command";
import type {FeedbackAnalysisWorkflow} from "@/modules/workflows/models/FeedbackAnalysisWorkflow";

/*
 * Represents update feedback analysis workflow model
 */
export class UpdateFeedbackAnalysisWorkflowCommand extends Command {

    /*
     * Product to be updated
     */
    public feedbackAnalysisWorkflow: FeedbackAnalysisWorkflow;

    constructor(feedbackAnalysisWorkflow: FeedbackAnalysisWorkflow) {
        super();

        this.feedbackAnalysisWorkflow = feedbackAnalysisWorkflow;
    }
}