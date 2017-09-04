import { Answer } from './answer';
import { Category } from './category';

export class Question {
    id: number;
    title: string;
    answers: Answer[];
    creationDate: Date;
    deletionDate: Date;
    isDeleted: boolean;
    categoryList: Category[];
    correctAnswer: Answer;
}
