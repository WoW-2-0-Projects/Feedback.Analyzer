import {Command} from "@/infrastructure/models/command/Command";
import type {Organization} from "@/modules/organizations/models/Organization";

export class DeleteOrganizationCommand extends Command{

    public organizationId:string;

    constructor(organizationId:string) {
        super();
        this.organizationId = organizationId;
    }

}