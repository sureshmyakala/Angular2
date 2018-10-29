import { Component } from '@angular/core';

import { IEmployee } from './employee';

import { EmployeeService } from './employee.service';

@Component({
    selector: 'list-employee',
    templateUrl: 'app/employee/employeeList.component.html',
    styleUrls: ['app/employee/employeeList.component.css'],
})


export class EmployeeListComponent {
    employees: IEmployee[];

    statusMessage: string = 'Loading data. Please wait...';

    selectedEmployeeCountRadioButton: string = 'All';

    constructor(private _employeeService: EmployeeService) { }

    ngOnInit() {
        this._employeeService.getEmployees().subscribe(employeeData => this.employees = employeeData,
            error => {
                this.statusMessage = 'Problem with the service. Please try again after sometime';
                console.error(error);
            });
    }

    getTotalEmployeesCount(): number {
        return this.employees.length;
    }

    getMaleEmployeesCount(): number {
        return this.employees.filter(e => e.gender === 'Male').length;
    }

    getFemaleEmployeesCount(): number {
        return this.employees.filter(e => e.gender === 'Female').length;
    }

    onEmployeeCountRadioButtonChange(selectedRadioButtonValue: string): void {
        this.selectedEmployeeCountRadioButton = selectedRadioButtonValue;
    }
}



//export class EmployeeListComponent {
//    employees: IEmployee[];
//    selectedEmployeeCountRadioButton: string = 'All';
//    constructor() {
//        this.employees = [
//            {
//                code: 'emp101', name: 'Tom', gender: 'Male',
//                annualSalary: 5500, dateOfBirth: '05/10/1988'
//            },
//            {
//                code: 'emp102', name: 'Alex', gender: 'Male',
//                annualSalary: 5700.95, dateOfBirth: '09/14/1982'
//            },
//            {
//                code: 'emp103', name: 'Mike', gender: 'Male',
//                annualSalary: 5900, dateOfBirth: '12/20/1979'
//            },
//            {
//                code: 'emp104', name: 'Mary', gender: 'Female',
//                annualSalary: 6500.826, dateOfBirth: '04/30/1980'
//            },
//            {
//                code: 'emp105', name: 'Nancy', gender: 'Female',
//                annualSalary: 6500.826, dateOfBirth: '12/15/1980'
//            },
//            {
//                code: 'emp106', name: 'Nancy123', gender: 'Male',
//                annualSalary: 6500.826, dateOfBirth: '12/15/1980'
//            },
//        ];
//    }


//    getTotalEmployeesCount(): number {
//        return this.employees.length;
//    }

//    getMaleEmployeesCount(): number {
//        return this.employees.filter(e => e.gender === 'Male').length;
//    }

//    getFemaleEmployeesCount(): number {
//        return this.employees.filter(e => e.gender === 'Female').length;
//    }

//    onEmployeeCountRadioButtonChange(selectedRadioButtonValue: string): void {
//        this.selectedEmployeeCountRadioButton = selectedRadioButtonValue;
//    }

//}

