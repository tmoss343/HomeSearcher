import { Injectable }    from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise'
import 'rxjs/add/operator/catch';

export class Region {
  id: number;
  country: string;
  state: string;
  county: string;
  city: string;
  cityurl: string;
  latitude: string;
  longitude: string;
}

export class NeighborhoodRegion {
  id: number;
  name: string;
  zindex: string;
  url: string;
  latitude: string;
  longitude: string;
}

export class ResponseRegion {
  parentRegion: Region;
  subregiontype: string
  regions: NeighborhoodRegion[];
}

@Injectable()
export class ZillowService {

  private headers = new Headers({'Content-Type': 'application/json'});

  constructor(private http: Http) { }

  getHomeData(): Observable<ResponseRegion>{
    return this.http
             .get(`http://www.zillow.com/webservice/GetRegionChildren.htm?zws-id=X1-ZWz197buben8jv_2ghte&state=mo&city=kansascity&childtype=neighborhood`)
             .map((response:Response) => response.json().data as ResponseRegion)
             .catch(this.handleError);
  }

  private handleError (error: any) {
    const errMsg = (error.message) ? error.message :
      error.status ? `${error.status} - ${error.statusText}` : 'No one likes our app';
    console.error(errMsg); // log to console instead
    return Observable.throw(errMsg);
  }
}
