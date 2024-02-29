/*
 * Represents standard problem details
 */
export class ProblemDetails {

    /*
     * Gets problem type
     */
    public type: string;

    /*
     * Gets problem title
     */
    public title: string;

    /*
     * Gets problem status code
     */
    public status: number;

    /*
    * Gets problem detail
     */
    public detail: string | null;

    constructor(type: string, title: string, status: number, detail: string | null) {
        this.type = type;
        this.title = title;
        this.status = status;
        this.detail = detail;
    }
}