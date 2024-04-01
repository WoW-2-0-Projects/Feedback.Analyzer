import {Command} from "@/infrastructure/models/command/Command";
import type {AnalysisPrompt} from "@/modules/prompts/models/AnalysisPrompt";

/*
 * Represents prompt command
 */
export  class CreatePromptCommand extends Command {

    /*
     * The AnalysisPrompt object to be created.
     */
    public prompt: AnalysisPrompt;

    constructor(prompt: AnalysisPrompt) {
        super();
        this.prompt = prompt;
    }
}