import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';


@Component({
    selector: 'http-error',
    templateUrl: 'http-error.component.html'
})

export class HttpErrorComponent implements OnInit {
    url: string;

    constructor(
        private location: Location,
        private router: Router
    ) { }

    goBack() {
        this.location.back();
    }

    ngOnInit() {
    }
}
