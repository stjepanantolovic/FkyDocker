import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './features/home/home.component';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { EnvelopeComponent } from './features/envelope/envelope.component';


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    NavComponent,
    HomeComponent,
    EnvelopeComponent
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
