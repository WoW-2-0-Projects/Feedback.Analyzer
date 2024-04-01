import  {FeedbackAnalysisPromptCategory} from "@/modules/prompts/models/FeedbackAnalysisPromptCategory";

/**
 * Represents an analysis prompt.
 */
export class AnalysisPrompt {
    /*
    * The ID of the prompt.
    */
    id!: string;

    /*
    * The type of feedback analysis prompt.
    */
    type!: FeedbackAnalysisPromptCategory;

    /*
    * The text content of the prompt.
    */
    prompt: string = '';

    /*
    * The version number of the prompt.
    */
    version: number;

    /*
    * The revision number of the prompt.
    */
    revision: number;

    /*
    * The ID of the category to which the prompt belongs.
    */
    categoryId!: string;

    /*
    * The date and time when the prompt was created.
    */
    createdTime!: Date;

    /*
    * The date and time when the prompt was last modified.
    */
    modifiedTime!: Date;

    constructor() {
        this.version = 0;
        this.revision = 0;
    }
}
