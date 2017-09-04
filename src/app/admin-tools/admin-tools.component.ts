import { pageInOut } from './../_animations/animations.component';
import { RouterOutlet } from '@angular/router';
import { Component, HostBinding } from '@angular/core';

@Component({
    selector: 'admin-tools',
    templateUrl: './admin-tools.component.html',
    styleUrls: ['./admin-tools.component.scss'],
    animations: [pageInOut]
})

export class AdminToolsComponent {
    @HostBinding('@pageInOut') pageInOut;
}
