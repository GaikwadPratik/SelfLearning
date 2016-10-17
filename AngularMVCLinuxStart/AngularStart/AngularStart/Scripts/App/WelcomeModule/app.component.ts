import { Component } from '@angular/core';

@Component({
    selector: 'first-app',
    template: `<h1> my first angular 2 app</h1><br/>
<h2>{{title}}<h2>`
})

export class AppComponent {
    title = "Hi there, I am from TS Class";
}