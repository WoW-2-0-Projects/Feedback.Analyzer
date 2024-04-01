import { ProblemDetails } from "@/infrastructure/apiClients/apiClientBase/ProblemDetails";
import {plainToClass} from "class-transformer";

/*
 * Represents API response
 */
export class ApiResponse<T> {
    public response!: T | null;

    /*
     * Gets error details, set only if request failed
     */
    public error!: ProblemDetails | null;

    /*
    * Gets response status code
    */
    public status: number;

    constructor(response: T | null, error: ProblemDetails | null, status: number) {
        this.response = response;
        this.status = status;

        if(error != null)
            this.error = plainToClass(ProblemDetails, error);
    }

    /*
     * Gets a value indicating whether the request was successful
     */
    public get isSuccess(): boolean {
        return this.status >= 200 && this.status < 300;
    }
}