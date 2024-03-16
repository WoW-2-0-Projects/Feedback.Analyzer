/**
 * LocalStorageService class provides utility methods for interacting with the browser's localStorage.
 * It allows setting, getting, removing, and clearing data stored in the localStorage.
 */
export class localStorageService {
    /**
     * Sets a value in the localStorage.
     */
    public Set<T>(key: string, value: T): void {
        localStorage.setItem(key, JSON.stringify(value));
    }

    /**
     * Retrieves a value from the localStorage.
     */
    public get<T>(key: string): T | null {
        const item = localStorage.getItem(key);
        return item ? JSON.parse(item) as T : null;
    }

    /**
     * Removes a value from the localStorage.
     * @param {string} key - The key of the value to remove.
     */
    public remove(key: string): void {
        localStorage.removeItem(key);
    }

    /**
     * Clears all data stored in the localStorage.
     */
    public clear(): void {
        localStorage.clear();
    }
}
