"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var employee_service_1 = require("./employee.service");
var EmployeeListComponent = /** @class */ (function () {
    function EmployeeListComponent(_employeeService) {
        this._employeeService = _employeeService;
        this.statusMessage = 'Loading data. Please wait...';
        this.selectedEmployeeCountRadioButton = 'All';
    }
    EmployeeListComponent.prototype.ngOnInit = function () {
        var _this = this;
        this._employeeService.getEmployees().subscribe(function (employeeData) { return _this.employees = employeeData; }, function (error) {
            _this.statusMessage = 'Problem with the service. Please try again after sometime';
            console.error(error);
        });
    };
    EmployeeListComponent.prototype.getTotalEmployeesCount = function () {
        return this.employees.length;
    };
    EmployeeListComponent.prototype.getMaleEmployeesCount = function () {
        return this.employees.filter(function (e) { return e.gender === 'Male'; }).length;
    };
    EmployeeListComponent.prototype.getFemaleEmployeesCount = function () {
        return this.employees.filter(function (e) { return e.gender === 'Female'; }).length;
    };
    EmployeeListComponent.prototype.onEmployeeCountRadioButtonChange = function (selectedRadioButtonValue) {
        this.selectedEmployeeCountRadioButton = selectedRadioButtonValue;
    };
    EmployeeListComponent = __decorate([
        core_1.Component({
            selector: 'list-employee',
            templateUrl: 'app/employee/employeeList.component.html',
            styleUrls: ['app/employee/employeeList.component.css'],
        }),
        __metadata("design:paramtypes", [employee_service_1.EmployeeService])
    ], EmployeeListComponent);
    return EmployeeListComponent;
}());
exports.EmployeeListComponent = EmployeeListComponent;
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
//# sourceMappingURL=employeeList.component.js.map