import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { MaterialModule, MdToolbarModule, MdCardModule, MdListModule, MdIconModule, MdGridListModule, MdButtonModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';

import { PropertyComponent } from './property.component';
import { PropertyRoutingModule } from './property-routing.module';
import { ZillowService } from '../zillow.service';
import 'hammerjs';

@NgModule({
  declarations: [
    PropertyComponent
  ],
  imports: [
    PropertyRoutingModule,
    CommonModule,
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
  providers: [ZillowService]
})
export class PropertyModule { }