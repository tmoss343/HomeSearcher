import { Component } from '@angular/core';
import { ZillowService, NeighborhoodRegion, DeepComp, HomeDetails } from './zillow.service';
import { MaterialModule, MdToolbarModule, MdCardModule, MdListModule, MdIconModule, MdGridListModule, MdButton } from '@angular/material';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app works!';
  public regionResponse: NeighborhoodRegion[] = new Array<NeighborhoodRegion>();
  public propResponse: HomeDetails[] = new Array<HomeDetails>();
  public compResponse: DeepComp[] = new Array<DeepComp>();
  private errorMessage: string;

  constructor(public zillowService: ZillowService) {
    this.getNeighborhood();
    this.getProperty();
  }

  private getNeighborhood() {
    this.zillowService.getNeighborhoodData()
        .subscribe(
          regions => this.regionResponse = regions as NeighborhoodRegion[],
          error => this.errorMessage = <any>error
        );
  }

  private getProperty() {
    this.zillowService.getPropertyData()
      .subscribe(
        props => this.propResponse = props as HomeDetails[],
        error => this.errorMessage = <any>error
      );
  }

  private getComps() {
    this.zillowService.getCompsData(`274998`)
      .subscribe(
        comp => this.compResponse = comp as DeepComp[],
        error => this.errorMessage = <any>error
      );
  }
}
