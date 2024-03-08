/*
 * Provides local storage management functionality
 */
export class LocalStorageService {

    /*
     * Sets an item in local storage
     */
    public set<T>(key: string, value: T): void {
        localStorage.setItem(key, JSON.stringify(value));
    }

    /*
     * Gets an item from local storage
     */
    public get<T>(key: string): T | null {
        const item = localStorage.getItem(key);
        return item ? JSON.parse(item) as T : null;
    }

    /*
     Removes an item from local storage
     */
    public remove(key: string): void {
        localStorage.removeItem(key);
    }

    /*
     * Clears local storage
     */
    public clear(): void {
        localStorage.clear();
    }
}