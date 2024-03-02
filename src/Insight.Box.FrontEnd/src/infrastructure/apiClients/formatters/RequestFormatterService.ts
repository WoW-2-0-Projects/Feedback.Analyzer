/*
 * Represents the request formatter service.
 */
import type {Query} from "@/infrastructure/models/query/Query";

export class RequestFormatterService {

    /*
     * Adds query parameters to the given url
     */
    public addQueryParams(url: string, query: Query<any>) {
        let formattedUrl = url;

        if(query) {
            formattedUrl += '?' + new URLSearchParams(this.buildQueryParams(query)).toString();
        }

        return formattedUrl;
    }

    /*
     * Builds query parameters from the given data
     */
    public buildQueryParams<T>(query: Query<T>): Record<string, string> {
        const queryParams: Record<string, string> = {};

        // Helper function for recursion
        const flattenQueryParams = (obj: any, parentKey: string = '') => {
            Object.entries(obj).forEach(([key, value]) => {
                if (value !== undefined && value !== null) {
                    const newKey = parentKey ? `${parentKey}.${key}` : key;
                    if (typeof value === 'object' && !(value instanceof Date)) {
                        // Recurse into sub-object
                        flattenQueryParams(value, newKey);
                    } else {
                        // Convert value to string and assign to the new key
                        queryParams[newKey] = value.toString();
                    }
                }
            });
        };

        // Start the recursion with the initial query object
        flattenQueryParams(query);

        return queryParams;
    }

}