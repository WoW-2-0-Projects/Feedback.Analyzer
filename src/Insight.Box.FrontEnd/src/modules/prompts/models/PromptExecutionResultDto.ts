/*
 * Represents the result of executing a prompt.
 */
export class PromptExecutionResultDto {
    /*
     * The ID of the prompt.
     */
    promptId!: string;

    /*
     * The version of the prompt.
     */
    version!: number;

    /*
     *  The revision number of the prompt.
     */
    revision!: number;

    /*
     * The total number of executions for the prompt.
     */
    executionsCount!: number;

    /*
     * The average execution time for the prompt.
     */
    averageExecutionTime!: number;

    /*
     * The average accuracy of the prompt's executions.
     */
    averageAccuracy!: number;
}
