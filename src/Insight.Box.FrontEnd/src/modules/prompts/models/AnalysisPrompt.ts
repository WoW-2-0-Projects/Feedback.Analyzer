import type {FeedbackAnalysisPromptType} from "@/modules/prompts/models/FeedbackAnalysisPromptType";

export class AnalysisPrompt {
    id: string | null;
    type: FeedbackAnalysisPromptType;
    prompt: string;
    version: number;
    revision: number;
    categoryId: string;
    createdTime: Date;
    modifiedTime: Date;

    constructor(type: FeedbackAnalysisPromptType, prompt: string) {
        this.type = type;
        this.prompt = prompt ?? '';
        this.version = 0;
        this.revision = 0;
        this.createdTime = new Date();
        this.modifiedTime = new Date();
    }
}