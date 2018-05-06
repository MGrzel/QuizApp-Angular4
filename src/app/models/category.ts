import { ChallengeCategory } from "./challengecategory";
import { CategoryQuestion } from "./categoryquestion";
import { environment } from "../../environments/environment";

export class Category {
    constructor() {
        this.id = environment.emptyGuid;
    }
    id: string;
    title: string;
    creationDate: Date;
    deletionDate: Date;
    isDeleted: boolean;
    challengeCategories: ChallengeCategory[];
    categoryQuestions: CategoryQuestion[];
}
