import { Category } from './category';
import { Challenge } from './challenge';
import { environment } from '../../environments/environment.prod';

export class ChallengeCategory {
    constructor() {
        this.challengeId = environment.emptyGuid;
        this.categoryId = environment.emptyGuid;
    }
    challenge: Challenge;
    challengeId: string;
    category: Category;
    categoryId: string;
    creationDate: Date;
    deletionDate: Date;
    isDeleted: boolean;
}
