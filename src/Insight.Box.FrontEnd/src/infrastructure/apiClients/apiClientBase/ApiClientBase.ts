import type {AxiosError, AxiosInstance, AxiosRequestConfig, AxiosResponse} from "axios";
import axios, {HttpStatusCode} from "axios";
import type {ProblemDetails} from "@/infrastructure/apiClients/apiClientBase/ProblemDetails";
import {ApiResponse} from "@/infrastructure/apiClients/apiClientBase/ApiResponse";
import {localStorageService} from "@/infrastructure/services/storage/LocalStorageService";
import {plainToClass} from "class-transformer";

/*
 * Base class for API clients
 */
export default class ApiClientBase {
    public readonly client: AxiosInstance;
    private readonly localStorageService: localStorageService;

    constructor(config: AxiosRequestConfig) {
        this.client = axios.create(config);
        this.localStorageService = new localStorageService();

        // Access token interceptor
        this.client.interceptors.request.use(async (config) => {
            const accessToken = this.localStorageService.get<string>("accessToken");
            if (accessToken) {
                config.headers.Authorization = `Bearer ${accessToken}`;
            }

            return config;
        });

        // Refresh token interceptor
        this.client.interceptors.response.use((response) => {
            return response;
        }, async (error) => {

            if (error.response.status == HttpStatusCode.Unauthorized && !error.retry) {

                error._retry = true;

                const refreshToken = this.localStorageService.get<string>("refreshToken");

                if (refreshToken) {
                    try {
                        // Make a request to the refresh token endpoint
                        const response = await axios.post('/auth/refresh-token', { refreshToken });
                        const newAccessToken = response.data.accessToken;

                        // Store the new access token in local storage
                        this.localStorageService.Set("accessToken", newAccessToken);

                        // Update the Authorization header
                        error.config.headers['Authorization'] = `Bearer ${newAccessToken}`;

                        // Retry the original request
                        return this.client.request(error.config);
                    } catch (refreshError) {
                    }
                }
            }

            return Promise.reject(error);

        });

        // Add response wrapper interceptor
        this.client.interceptors.response.use(<TResponse>(response: AxiosResponse<TResponse>) => {
                let data = new ApiResponse(response.data as TResponse, null, response.status);

                if(response.config.mapper != null && response.data != null)
                    data.response = response.config.mapper(response.data);

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
        return (await this.client.get<ApiResponse<T>>(url, config)).data as ApiResponse<T>;
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