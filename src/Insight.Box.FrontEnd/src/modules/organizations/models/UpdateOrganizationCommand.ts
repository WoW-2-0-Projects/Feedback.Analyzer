import {Command} from "@/infrastructure/models/command/Command";
import type {Organization} from "@/modules/organizations/models/Organization";

export class  UpdateOrganizationCommand extends Command{

    public organization:Organization;

    public constructor(organization: Organization) {
        super();
        this.organization = organization;
    }

}