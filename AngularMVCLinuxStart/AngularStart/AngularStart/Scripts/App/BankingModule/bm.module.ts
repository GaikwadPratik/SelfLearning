import { NgModule } from '@angular/core'
import { BrowserModule } from '@angular/platform-browser'
import { BmWelcomeComponent } from './WelcomeComponent/bm.welcomecomponent'

@NgModule({
    imports: [BrowserModule],
    declarations: [BmWelcomeComponent],
    bootstrap: [BmWelcomeComponent]
})

export class BmModule { }