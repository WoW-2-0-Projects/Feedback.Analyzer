import type {FeedbackAnalysisPromptType} from "@/modules/prompts/models/FeedbackAnalysisPromptType";

export class FeedbackAnalysisPrompt {
    id: string;
    type: FeedbackAnalysisPromptType;
    prompt: string;
    majorVersion: number;
    minorVersion: number;
    createdTime: Date;
    modifiedTime: Date;

    constructor(type: FeedbackAnalysisPromptType, prompt: string) {
        this.type = type;
        this.prompt = prompt ?? '';
        this.majorVersion = 0;
        this.minorVersion = 0;
        this.createdTime = new Date();
        this.modifiedTime = new Date();
    }
}