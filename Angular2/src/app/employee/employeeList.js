"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var EmployeeListComponent = /** @class */ (function () {
    function EmployeeListComponent() {
        this.employees = [
            {
                code: '101', name: 'AAA', gender: 'Male', salary: '60000', dateBirth: '18-06-1991'
            },
            {
                code: '102', name: 'BBB', gender: 'FeMale', salary: '50000', dateBirth: '15-06-1992'
            },
            {
                code: '103', name: 'CCC', gender: 'FeMale', salary: '45000', dateBirth: '15-06-1993'
            },
            {
                code: '104', name: 'DDD', gender: 'Male', salary: '65000', dateBirth: '15-06-1992'
            },
            {
                code: '105', name: 'EEE', gender: 'Male', salary: '70000', dateBirth: '15-06-1990'
            },
        ];
    }
    EmployeeListComponent = __decorate([
        core_1.Component({
            selector: 'list-employee',
            templateUrl: 'app/employee/employeeList.component.html',
            styleUrls: ['app/employee/employeeList.component.css']
        })
    ], EmployeeListComponent);
    return EmployeeListComponent;
}());
exports.EmployeeListComponent = EmployeeListComponent;
//# sourceMappingURL=employeeList.js.map