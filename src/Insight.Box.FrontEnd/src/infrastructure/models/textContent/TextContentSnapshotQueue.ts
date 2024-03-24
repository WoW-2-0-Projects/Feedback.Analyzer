/**
 * Represents a queue for managing text content snapshots, allowing undo and redo operations.
 */
export class TextContentSnapshotQueue {

    /** Array to store the history of text content snapshots. */
    private history: Array<string> = [];

    /** Index to keep track of the current position in the history array. */
    private index = -1;

    /**
     * Undo the last text content snapshot and move to the previous state in the history.
     * @returns {string} The text content snapshot before the undo operation.
     */
    public undo(): string {
        if (this.index > 0) {
            this.index--;
            return this.history[this.index];
        }

        return '';
    }

    /**
     * Redo the last undone text content snapshot and move to the next state in the history.
     * @returns {string} The text content snapshot after the redo operation.
     */
    public redo(): string {
        if (this.index < this.history.length - 1)
            this.index++;

        return this.history[this.index];
    }

    /**
     * Record a new text content snapshot.
     * @param {string} value - The text content snapshot to record.
     */
    public record(value: string): void {
        this.history.push(value);
        this.index = this.history.length - 1;

        // Optional: Limit history stack size for memory optimization
        if (this.history.length > 50) { // Adjust the limit as needed
            this.history.shift(); // Remove oldest state
            this.index--;
        }
    }
}
