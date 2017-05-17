import { Component } from '@angular/core';
import { ZillowService, ResponseRegion } from './zillow.service';
import { MaterialModule, MdToolbarModule, MdCardModule, MdListModule, MdIconModule } from '@angular/material';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app works!';
  public regionResponse: ResponseRegion = new ResponseRegion();
  private errorMessage: string;

  constructor(public zillowService: ZillowService) {}

  private getNeighborhood() {
    this.zillowService.getHomeData()
        .subscribe(
          regions => this.regionResponse = regions as ResponseRegion,
          error => this.errorMessage = <any>error
        );
  }
}
