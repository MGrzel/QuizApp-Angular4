import { ClientQuiz } from './clientquiz';
import { Challenge } from './challenge';

export class Session {
    id: number;
    date: Date;
    isActive: boolean;
    challenge: Challenge;
    clientQuiz: ClientQuiz[];
    creationDate: Date;
    deletionDate: Date;
    isDeleted: boolean;
}
