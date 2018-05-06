import { ClientQuiz } from './clientquiz';
import { Challenge } from './challenge';
import { environment } from '../../environments/environment';

export class Session {
    constructor() {
        this.id = environment.emptyGuid;
        this.challengeId = environment.emptyGuid;
    }
    id: string;
    date: Date;
    isActive: boolean;
    challenge: Challenge;
    challengeId: string;
    clientQuiz: ClientQuiz[];
    creationDate: Date;
    deletionDate: Date;
    isDeleted: boolean;
}
