import { Injectable }    from '@angular/core';
import { Headers, Http, Response, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/catch';

export class NeighborhoodRegion {
  id: number;
  name: string;
  url: string;
  latitude: string;
  longitude: string;
}

export class DeepComp {
  id: number;
  latitude: number;
  longitude: number;
  name: string;
  url: string;
  neighborhoodId: number;
}

export class HomeDetails {
  id: string;
  longitude: string;
  latitude: string;
  lastSoldPrice: string;
  lastSoldDate: string;
  finishedSqFt: string;
  bathrooms: string;
  bedrooms: string;
  zestimate: string;
  lastUpdated: string;
  address: string;
  url: string;
  neighborhood: string;
}

@Injectable()
export class ZillowService {

  private headers = new Headers(
  {
    'Content-Type': 'application/json'
  });

  constructor(private http: Http) { }

  getNeighborhoodData():  Observable<any[]> {
    return this.http
     .get(`http://localhost:5000/houses/kcregions`, new RequestOptions({headers: this.headers}))
     .map((response: Response) => response.json())
     .catch(this.handleError);
  }

  getPropertyData():  Observable<any[]> {
    return this.http
      .get(`http://localhost:5000/houses/propertysearchresults`, new RequestOptions({headers: this.headers}))
      .map((response: Response) => response.json())
      .catch(this.handleError);
  }

  getCompsData(id: string):  Observable<any[]> {
    return this.http
      .get(`http://localhost:5000/houses/comps?id=` + id, new RequestOptions({headers: this.headers}))
      .map((response: Response) => response.json())
      .catch(this.handleError);
  }

  private handleError (error: any) {
    const errMsg = (error.message) ? error.message :
      error.status ? `${error.status} - ${error.statusText}` : 'No one likes our app';
    console.error(errMsg); // log to console instead
    return Observable.throw(errMsg);
  }
}
