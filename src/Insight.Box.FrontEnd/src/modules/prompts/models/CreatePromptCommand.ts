import {Command} from "@/infrastructure/models/command/Command";
import type {AnalysisPrompt} from "@/modules/prompts/models/AnalysisPrompt";

/*
 * Represents create product model
 */
export class CreatePromptCommand extends Command {

    /*
     * Product to be created
     */
    public prompt: AnalysisPrompt;

    constructor(prompt: AnalysisPrompt) {
        super();

        this.prompt = prompt;
    }
}