import { Component, OnInit, OnDestroy } from '@angular/core';
import { ZillowService, NeighborhoodRegion, DeepComp, HomeDetails } from '../zillow.service';
import { MaterialModule, MdToolbarModule, MdCardModule, MdListModule, MdIconModule, MdGridListModule, MdButton } from '@angular/material';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'property',
  templateUrl: './property.component.html',
  styleUrls: ['./property.component.css']
})
export class PropertyComponent implements OnInit, OnDestroy {
  title = 'Properties';
  public propResponse: HomeDetails[] = new Array<HomeDetails>();
  public compResponse: DeepComp[] = new Array<DeepComp>();
  private errorMessage: string;
  id: number;
  private routeSub: any;

  constructor(public zillowService: ZillowService, private route: ActivatedRoute) {
    this.getProperty();
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

  ngOnInit() {
    this.routeSub = this.route.params.subscribe(params => {
       this.id = +params['id']; // (+) converts string 'id' to a number

       // In a real app: dispatch action to load the details here.
    });
  }

  ngOnDestroy() {
    this.routeSub.unsubscribe();
  }
}
