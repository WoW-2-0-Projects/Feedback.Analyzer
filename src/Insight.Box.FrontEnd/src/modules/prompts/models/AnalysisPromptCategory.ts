import type {FeedbackAnalysisPromptCategory} from "@/modules/prompts/models/FeedbackAnalysisPromptCategory";
import type {AnalysisPrompt} from "@/modules/prompts/models/AnalysisPrompt";

/**
 * Represents a category for analysis prompts.
 */
export class AnalysisPromptCategory {
    /*
    * The ID of the category.
    */
    id!: string;

    /*
    * The type of feedback analysis prompt associated with the category.
    */
    type!: FeedbackAnalysisPromptCategory;

    /*
    * The display name of the feedback analysis prompt type.
    */
    typeDisplayName!: string;

    /*
    * The ID of the selected prompt within the category.
    */
    selectedPromptId!: string;

    /*
    * The version of the selected prompt within the category.
    */
    selectedPromptVersion!: string;

    /*
    * The total number of prompts within the category.
    */
    promptsCount!: number;

    /*
    * An array containing the prompts within the category.
    */
    prompts!: Array<AnalysisPrompt>;
}
