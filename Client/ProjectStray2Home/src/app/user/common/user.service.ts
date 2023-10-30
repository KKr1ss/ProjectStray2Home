import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../../environments/environment";
import { UserProfile } from "./models/user-profile";

@Injectable({
  providedIn: 'root',
})
export class UserService {
  url = environment.baseUrl;
  constructor( protected http: HttpClient) { }

  getProfileDetails(username: string) : Observable<UserProfile> {
    let customUrl = this.url + "api/Account/GetProfileDetails/" + username
    return this.http.get<UserProfile>(customUrl);
  }

  getAccountDetails(): Observable<UserProfile> {
    let customUrl = this.url + "api/Account/GetAccountDetails/"
    return this.http.get<UserProfile>(customUrl);
  }

}
