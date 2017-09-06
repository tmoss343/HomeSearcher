import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { PropertyComponent } from './property.component';

export const propertyRoutes: Routes = [
  {
    path: '', component: PropertyComponent
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(propertyRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class PropertyRoutingModule { }
