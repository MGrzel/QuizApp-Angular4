import { QuizType } from './quiztype';
import { Color } from './color';
import { Category } from './category';
import { ChallengeCategory } from './challengecategory';
import { environment } from '../../environments/environment';


export class Challenge {
    constructor() {
        this.id = environment.emptyGuid;
        this.colorId = environment.emptyGuid;
        this.quizTypeId = environment.emptyGuid;
    }
    id: string;
    title: string;
    questionAmount: number;
    color: Color;
    colorId: string;
    quizType: QuizType;
    quizTypeId: string;
    creationDate: Date;
    deletionDate: Date;
    isDeleted: boolean;
    categoryList: ChallengeCategory[];
}
