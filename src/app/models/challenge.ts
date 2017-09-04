import { QuizType } from './quiztype';
import { Color } from './color';
import { Category } from './category';


export class Challenge {
    id: number;
    title: string;
    questionAmount: number;
    color: Color;
    quizType: QuizType;
    creationDate: Date;
    deletionDate: Date;
    isDeleted: boolean;
    categoryList: Category[];
}
