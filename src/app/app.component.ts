import { Component } from '@angular/core';
import { ZillowService, NeighborhoodRegion } from './zillow.service';
import { MaterialModule, MdToolbarModule, MdCardModule, MdListModule, MdIconModule } from '@angular/material';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app works!';
  public regionResponse: NeighborhoodRegion[] = new Array<NeighborhoodRegion>();
  private errorMessage: string;

  constructor(public zillowService: ZillowService) {
    this.getNeighborhood();
  }


  private getNeighborhood() {
    this.zillowService.getHomeData()
        .subscribe(
          regions => this.regionResponse = regions as NeighborhoodRegion[],
          error => this.errorMessage = <any>error
        );
  }
}
