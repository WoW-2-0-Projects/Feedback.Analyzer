/**
 * Represents a customer feedback entity with audit information.
 */
export class CustomerFeedback {
    public id: string;

    /**
     * The unique identifier of the product associated with this feedback.
     */
    public productId: string;

    /**
     * The comment provided in the feedback.
     */
    public comment: string;

    /**
     * The username associated with the feedback.
     */
    public userName: string;
}