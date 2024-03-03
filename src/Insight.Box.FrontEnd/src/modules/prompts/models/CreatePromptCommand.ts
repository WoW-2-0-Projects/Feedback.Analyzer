import {Command} from "@/infrastructure/models/command/Command";
import type {FeedbackAnalysisPrompt} from "@/modules/prompts/models/AnalysisPrompt";

/*
 * Represents create product model
 */
export class CreatePromptCommand extends Command {

    /*
     * Product to be created
     */
    public prompt: FeedbackAnalysisPrompt;

    constructor(prompt: FeedbackAnalysisPrompt) {
        super();

        this.prompt = prompt;
    }
}