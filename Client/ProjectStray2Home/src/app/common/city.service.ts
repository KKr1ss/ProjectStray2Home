import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { City } from "./city";

@Injectable({
  providedIn: 'root',
})
export class CityService {
  url = environment.baseUrl + "api/City/";

  constructor(private http: HttpClient) { }

  get(id: number): Observable<City> {
    let customUrl = this.url + id;
    return this.http.get<City>(customUrl);
  }

  getCities(): Observable<City[]> {
    return this.http.get<City[]>(this.url);
  }
}
