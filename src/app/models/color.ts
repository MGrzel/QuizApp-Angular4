import { environment } from "../../environments/environment";

export class Color {
    constructor() {
        this.id = environment.emptyGuid;
    }
    id: string;
    title: string;
    value: string;
    creationDate: Date;
    deletionDate: Date;
    isDeleted: boolean;
}
