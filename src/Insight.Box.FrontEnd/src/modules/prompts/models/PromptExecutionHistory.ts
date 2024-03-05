/**
 * Represents a data transfer object (DTO) for a prompt execution history record.
 */
export interface PromptsExecutionHistory {
    /**
     * Unique identifier of the prompt execution history record.
     */
    promptsExecutionHistoryId: string; // Use 'string' for GUIDs in TypeScript

    /**
     * Unique identifier of the associated prompt.
     */
    promptId: string;

    /**
     * The result of the prompt execution (optional).
     */
    result?: string | null;

    /**
     * Any exception that occurred during the prompt execution (optional).
     */
    exception?: string | null;

    /**
     * The timestamp of when the prompt execution occurred.
     */
    executionTime: Date;

    /**
     * The duration of the prompt execution in seconds.
     */
    executionDurationInSeconds: number;
}
