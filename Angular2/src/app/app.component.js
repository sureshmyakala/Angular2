"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var AppComponent = /** @class */ (function () {
    function AppComponent() {
    }
    AppComponent = __decorate([
        core_1.Component({
            selector: 'my-app',
            template: "\n                    <div style=\"padding:5px\">\n                        <ul class=\"nav nav-tabs\">\n                            <li routerLinkActive=\"active\">\n                                <a routerLink=\"home\">Home</a>\n                            </li>\n                            <br/>\n                            <li routerLinkActive=\"active\">\n                                <a routerLink=\"employees\">Employees</a>\n                            </li>\n                        </ul>\n                        <br/>\n                        <router-outlet></router-outlet>\n                    </div>"
            //template: `<list-employee></list-employee>`
        })
    ], AppComponent);
    return AppComponent;
}());
exports.AppComponent = AppComponent;
//@Component({
//    selector: 'my-app',
//    template: `Name:<input [(ngModel)]='name'>
//<br>You entered : {{name}}`
//})
//export class AppComponent {
//    name: string = 'Tom';
//}
//import { Component } from '@angular/core';
//@Component({
//    selector: 'my-app',
//    template: `<div>
//                    <h1>{{pageHeader}}</h1><my-employee></my-employee>
//               </div>`
//})
//export class AppComponent {
//    pageHeader: string = 'Employee Details';
//}
//export class AppComponent {
//    onClick(): void {
//        alert("Button Clicked");
//    }
//    isBold: boolean = true;
//    fontSize: number = 20;
//    isItalic: boolean = true;
//    addStyles() {
//        let styles = {
//            'font-weight': this.isBold ? 'bold' : 'normal',
//            'font-style': this.isItalic ? 'italic' : 'normal',
//            'font-size.px': this.fontSize
//        };
//        return styles;
//    }
//}
//# sourceMappingURL=app.component.js.map