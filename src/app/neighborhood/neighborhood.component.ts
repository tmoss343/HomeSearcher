import { Component } from '@angular/core';
import { ZillowService, NeighborhoodRegion, DeepComp, HomeDetails } from '../zillow.service';
import { MaterialModule, MdToolbarModule, MdCardModule, MdListModule, MdIconModule, MdGridListModule, MdButton } from '@angular/material';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'neighborhood',
  templateUrl: './neighborhood.component.html',
  styleUrls: ['./neighborhood.component.css']
})
export class NeighborhoodComponent {
  title = 'neighborhoods';
  public regionResponse: NeighborhoodRegion[] = new Array<NeighborhoodRegion>();
  private errorMessage: string;

  constructor(public zillowService: ZillowService) {
    this.getNeighborhood();
  }

  private getNeighborhood() {
    this.zillowService.getNeighborhoodData()
        .subscribe(
          regions => this.regionResponse = regions as NeighborhoodRegion[],
          error => this.errorMessage = <any>error
        );
  }
}
