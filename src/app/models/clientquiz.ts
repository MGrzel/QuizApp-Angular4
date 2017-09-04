import { Color } from './color';
import { Answer } from './answer';
import { Question } from './question';

export class ClientQuiz {
    id: number;
    sessionId: number;
    isCorrect: boolean;
    order: number;
    selectedAnswer: Answer;
    question: Question;
    creationDate: Date;
    deletionDate: Date;
    isDeleted: boolean;
}
