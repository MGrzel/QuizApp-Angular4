import { Answer } from './answer';
import { Category } from './category';
import { CategoryQuestion } from './categoryquestion';
import { environment } from '../../environments/environment.prod';

export class Question {
    constructor() {
        this.id = environment.emptyGuid;
    }
    id: string;
    title: string;
    answers: Answer[];
    creationDate: Date;
    deletionDate: Date;
    isDeleted: boolean;
    categoryList: CategoryQuestion[];
}
