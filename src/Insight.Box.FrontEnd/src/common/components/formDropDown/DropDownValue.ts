/*
 * Represents a key-value pair for a dropdown
 */
export class DropDownValue<TKey, TValue> {
    /*
     * The key of the dropdown
     */
    public key: TKey;

    /*
     * The value of the dropdown
     */
    public value: TValue;

    constructor(key: TKey, value: TValue) {
        this.key = key;
        this.value = value;
    }

    public static create<TKey, TValue>(key: TKey, value: TValue): DropDownValue<TKey, TValue> {
        return new DropDownValue(key, value);
    }
}