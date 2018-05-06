import { environment } from '../../environments/environment.prod';
import { Question } from './question';

export class Answer {
    constructor() {
        this.id = environment.emptyGuid;
        this.questionId = environment.emptyGuid;
    }
    id: string;
    questionId: string;
    question: Question;
    title: string;
    creationDate: Date;
    deletionDate: Date;
    isDeleted: boolean;
    isCorrect: boolean;
}
