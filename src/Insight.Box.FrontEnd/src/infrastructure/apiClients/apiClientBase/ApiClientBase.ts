import type { AxiosError, AxiosInstance, AxiosRequestConfig, AxiosResponse } from "axios";
import axios from "axios";
import type { ProblemDetails } from "@/infrastructure/apiClients/apiClientBase/ProblemDetails";
import { ApiResponse } from "@/infrastructure/apiClients/apiClientBase/ApiResponse";

export default class  ApiClientBase {

    public  readonly  client: AxiosInstance;
    public mapResponse!: <T>(response: ApiResponse<T>) => ApiResponse<T>

    constructor(config: AxiosRequestConfig) {
        this.client = axios.create(config);


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
}