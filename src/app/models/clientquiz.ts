import { Color } from './color';
import { Answer } from './answer';
import { Question } from './question';
import { Session } from './session';
import { environment } from '../../environments/environment.prod';

export class ClientQuiz {
    constructor() {
        this.id = environment.emptyGuid;
        this.questionId = environment.emptyGuid;
        this.sessionId = environment.emptyGuid;
    }
    id: string;
    session: Session;
    sessionId: string;
    order: number;
    selectedAnswer: Answer;
    selectedAnswerId: string;
    question: Question;
    questionId: string;
    creationDate: Date;
    deletionDate: Date;
    isDeleted: boolean;
}
