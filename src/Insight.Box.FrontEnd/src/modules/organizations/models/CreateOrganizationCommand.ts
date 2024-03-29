import {Command} from "@/infrastructure/models/command/Command";
import type {Organization} from "@/modules/organizations/models/Organization";

/*
 * Represents a command to create an organization.
 */
export class CreateOrganizationCommand extends Command {

   /*
    * Organization to created
    */
    public organization: Organization;

    constructor(organization: Organization)
    {
        super();
        this.organization = organization
        }

}