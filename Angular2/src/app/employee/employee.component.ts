import { Component } from '@angular/core';

@Component({
    selector: 'my-employee',
    templateUrl: 'app/employee/employee.component.html',
    styleUrls: ['app/employee/employee.component.css']
})
export class EmployeeComponent {
    columnSpan: number = 2;
    firstName: string = 'Tcdxzf';
    lastName: string = 'dasa';
    gender: string = 'Male';
    age: number = 25;
    ShowDetails: boolean = false;

    toggleDetails() {
        this.ShowDetails = !this.ShowDetails;
    }
}