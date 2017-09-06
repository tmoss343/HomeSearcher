import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { NeighborhoodComponent } from './neighborhood.component';

export const neighborhoodRoutes: Routes = [
  {
    path: '', component: NeighborhoodComponent
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(neighborhoodRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class NeighborhoodRoutingModule { }
