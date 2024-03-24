export class TextContentSnapshotQueue {

    private history: Array<string> = [];
    private index = -1;

    public undo(): string {
        if (this.index > 0) {
            this.index--;
            return this.history[this.index];
        }

        return '';
    }

    public redo(): string {
        if (this.index < this.history.length - 1)
            this.index++;

        return this.history[this.index];
    }

    public record(value: string): void {
        this.history.push(value);
        this.index = this.history.length - 1;

        // Optional: Limit history stack size for memory optimization
        if (this.history > 50) { // Adjust the limit as needed
            this.history.shift(); // Remove oldest state
            this.index--;
        }
    }
}