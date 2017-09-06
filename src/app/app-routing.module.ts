import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { PropertyModule } from './property/property.module';
import { NeighborhoodModule } from './neighborhood/neighborhood.module';

export function getPropertyModuleChildren() {
    return PropertyModule;
}

export function getNeighborhoodModuleChildren() {
    return NeighborhoodModule;
}

@NgModule({
  imports: [
    RouterModule.forRoot([
      /* define app module routes here, e.g., to lazily load a module
       (do not place feature module routes here, use an own -routing.module.ts in the feature instead)
       */
      { path: '', loadChildren: getNeighborhoodModuleChildren },
      { path: 'property/:id', loadChildren: getPropertyModuleChildren },
      { path: '**', loadChildren: getNeighborhoodModuleChildren }
    ])
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }