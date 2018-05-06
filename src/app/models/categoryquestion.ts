import { Question } from './question';
import { Category } from './category';
import { environment } from '../../environments/environment';

export class CategoryQuestion {
    constructor() {
        this.categoryId = environment.emptyGuid;
        this.questionId = environment.emptyGuid;
    }
    question: Question;
    questionId: string;
    category: Category;
    categoryId: string;
    creationDate: Date;
    deletionDate: Date;
    isDeleted: boolean;
}
