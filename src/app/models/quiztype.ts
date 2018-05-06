import { environment } from "../../environments/environment.prod";

export class QuizType {
    constructor() {
        this.id = environment.emptyGuid;
    }
    id: string;
    title: string;
    description: string;
    creationDate: Date;
    deletionDate: Date;
    isDeleted: boolean;
}
