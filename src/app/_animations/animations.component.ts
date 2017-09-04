import {
    trigger,
    state,
    style,
    animate,
    transition
} from '@angular/animations';



export const fadeInOut = trigger('fadeInOut', [
    state('*', style({ opacity: 1, transform: 'translateX(0)' })),
    transition('void => *', [
        style({
            opacity: 0,
            transform: 'translateY(-10%)'
        }),
        animate('0.3s ease-in')
    ]),
    transition('* => leave', [
        animate('0.3s ease-out', style({
            opacity: 0.5,
            transform: 'translateY(100%)'
        }))
    ]),
    transition('* => out', [
        animate('0.4s 0.5s ease-out', style({
            opacity: 0.5,
        }))
    ]),
    transition('* => in', [
        style({
            opacity: 0,
        }),
        animate('0.2s ease-in')
    ])
]);

export const pageInOut = trigger('pageInOut', [
    state('*', style({ opacity: 1, transform: 'translateX(0)' })),
    transition('* => in', [
        style({
            opacity: 0.5,
            transform: 'translateY(-10%)'
        }),
        animate('0.3s ease-in')
    ]),
    transition('* => out', [
        animate('0.3s ease-out', style({
            opacity: 0.5,
            transform: 'translateY(30%)'
        }))
    ])
]);

export const elementInOut = trigger('elementInOut', [
    state('*', style({ opacity: 1, transform: 'translateX(0)' })),
    transition('void => *', [
        style({
            opacity: 0.5,
            transform: 'translateY(-10%)'
        }),
        animate('0.3s ease-in')
    ]),
    transition('* => void', [
        animate('0.3s ease-out', style({
            opacity: 0.5,
            transform: 'translateY(30%)'
        }))
    ])
]);
