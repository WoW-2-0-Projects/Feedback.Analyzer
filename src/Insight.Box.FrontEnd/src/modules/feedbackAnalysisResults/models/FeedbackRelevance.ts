/**
 * Represents the relevance and content of feedback.
 */
export class FeedbackRelevance {
    /**
     * A value indicating whether the feedback is relevant.
     */
    isRelevant: boolean;

    /**
     * The extracted relevant content from the feedback.
     */
    extractedRelevantContent: string;

    /**
     * The personally identifiable information (PII) redacted content of the feedback.
     */
    piiRedactedContent: string;

    /**
     * An array of recognized languages in the feedback content.
     */
    recognizedLanguages: string[];
}