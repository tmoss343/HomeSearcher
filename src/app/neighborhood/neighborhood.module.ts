import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { MaterialModule, MdToolbarModule, MdCardModule, MdListModule, MdIconModule, MdGridListModule, MdButtonModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';

import { NeighborhoodComponent } from './neighborhood.component';
import { NeighborhoodRoutingModule } from './neighborhood-routing.module';
import { ZillowService } from '../zillow.service';
import 'hammerjs';

@NgModule({
  declarations: [
    NeighborhoodComponent
  ],
  imports: [
    NeighborhoodRoutingModule,
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
export class NeighborhoodModule { }