import type { AxiosError, AxiosInstance, AxiosRequestConfig, AxiosResponse } from "axios";
import axios from "axios";
import type { ProblemDetails } from "@/infrastructure/apiClients/apiClientBase/ProblemDetails";
import { ApiResponse } from "@/infrastructure/apiClients/apiClientBase/ApiResponse";

/*
 * Base class for API clients
 */
export default class ApiClientBase {
    /*
     * Axios client instance
     */
    public readonly client: AxiosInstance;

    /*
     * Map response delegate
     */
    public mapResponse!: <T>(response: ApiResponse<T>) => ApiResponse<T>

    constructor(config: AxiosRequestConfig) {
        this.client = axios.create(config);

        // Add response wrapper interceptor
        this.client.interceptors.response.use(<TResponse>(response: AxiosResponse<TResponse>) => {
                let data = new ApiResponse(response.data as TResponse, null, response.status);

                if (this.mapResponse != null)
                    data = this.mapResponse(data);

                return {
                    ...response,
                    data: data
                }

            },
            (error: AxiosError) => {
                return {
                    ...error,
                    data: new ApiResponse(null, error.response?.data as ProblemDetails, error.response?.status ?? 500)
                };
            }
        );
    }

    /**
     * Performs an asynchronous GET request using the internal Axios client, extracting
     * the API response data.
     *
     * @param {string} url - The URL endpoint to send the GET request to.
     * @param {AxiosRequestConfig} [config] - Optional configuration for the Axios request.
     * @returns {Promise<T>} A Promise that resolves to the extracted data from the
     *                       ApiResponse<T> object.
     */
    public async getAsync<T>(url: string, config?: AxiosRequestConfig): Promise<ApiResponse<T>> {
        return (await this.client.get<ApiResponse<T>>(url, config)).data;
    }

    /**
     * Performs an asynchronous POST request using the internal Axios client,
     * extracting the API response data.
     *
     * @param {string} url - The URL endpoint to send the POST request to.
     * @param {any} [data] - Optional data to be included in the POST request body.
     * @param {AxiosRequestConfig} [config] - Optional configuration for the Axios request.
     * @returns {Promise<T>} A Promise that resolves to the extracted data from the
     *                       ApiResponse<T> object.
     */
    public async postAsync<T>(url: string, data?: any, config?: AxiosRequestConfig): Promise<ApiResponse<T>> {
        return (await this.client.post<ApiResponse<T>>(url, data, config)).data;
    }

    /**
     * Performs an asynchronous PUT request using the internal Axios client,
     * extracting the API response data.
     *
     * @param {string} url - The URL endpoint to send the PUT request to.
     * @param {any} [data] - Optional data to be included in the PUT request body.
     * @param {AxiosRequestConfig} [config] - Optional configuration for the Axios request.
     * @returns {Promise<T>} A Promise that resolves to the extracted data from the
     *                       ApiResponse<T> object.
     */
    public async putAsync<T>(url: string, data?: any, config?: AxiosRequestConfig): Promise<ApiResponse<T>> {
        return (await this.client.put<ApiResponse<T>>(url, data, config)).data;
    }

    /**
     * Performs an asynchronous DELETE request using the internal Axios client,
     * extracting the API response data.
     *
     * @param {string} url - The URL endpoint to send the DELETE request to.
     * @param {AxiosRequestConfig} [config] - Optional configuration for the Axios request.
     * @returns {Promise<T>} A Promise that resolves to the extracted data from the
     *                       ApiResponse<T> object.
     */
    public async deleteAsync<T>(url: string, config?: AxiosRequestConfig): Promise<ApiResponse<T>> {
        return (await this.client.delete<ApiResponse<T>>(url, config)).data;
    }
}