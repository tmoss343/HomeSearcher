import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { MaterialModule, MdToolbarModule, MdCardModule, MdListModule, MdIconModule, MdGridListModule, MdButtonModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';

import { AppComponent } from './app.component';
import { ZillowService } from './zillow.service';
import 'hammerjs';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    FlexLayoutModule,
    HttpModule,
    MdCardModule,
    MdToolbarModule,
    MdListModule,
    MdIconModule,
    MdGridListModule,
    MdButtonModule
  ],
  providers: [ZillowService],
  bootstrap: [AppComponent]
})
export class AppModule { }
