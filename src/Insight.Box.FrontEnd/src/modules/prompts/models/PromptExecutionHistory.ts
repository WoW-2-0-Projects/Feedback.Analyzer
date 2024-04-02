/**
 * Represents a data transfer object (DTO) for a prompt execution history record.
 */
export class PromptsExecutionHistory {
    /**
     * Unique identifier of the prompt execution history record.
     */
    public id!: string; // Use 'string' for GUIDs in TypeScript

    /**
     * Unique identifier of the associated prompt.
     */
    public promptId!: string;

    /**
     * The result of the prompt execution (optional).
     */
    public result?: string | null;

    /**
     * Any exception that occurred during the prompt execution (optional).
     */
    public exception?: string | null;

    /**
     * The timestamp of when the prompt execution occurred.
     */
    public executionTime!: Date;

    /**
     * The duration of the prompt execution in seconds.
     */
    public executionDurationInMilliseconds!: number;
}
