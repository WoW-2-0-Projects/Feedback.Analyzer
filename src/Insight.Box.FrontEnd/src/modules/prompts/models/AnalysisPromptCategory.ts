import type {FeedbackAnalysisPromptType} from "@/modules/prompts/models/FeedbackAnalysisPromptType";
import type {AnalysisPrompt} from "@/modules/prompts/models/AnalysisPrompt";

export class AnalysisPromptCategory {
    id: string;
    type: FeedbackAnalysisPromptType;
    typeDisplayName: string;
    selectedPromptId: string;
    promptsCount: number;
    prompts: Array<AnalysisPrompt>;
}