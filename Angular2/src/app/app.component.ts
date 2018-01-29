import { Component } from '@angular/core';

@Component({
    selector: 'my-app',
    template: `Name:<input [(ngModel)]='name'>
<br>You entered : {{name}}`
})

export class AppComponent {
    name: string = 'Tom';
}


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
