/* eslint-disable prefer-const */
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subject, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { LoginRequest } from './models/login-request';
import { LoginResult } from './models/login-result';
import { RegisterRequest } from './models/register-request';
import { Roles } from '../../shared/enums/enums';
import { formatDate } from '@angular/common'
import { APIResult } from '../../common/models/api-result';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  private tokenKey: string = "token";
  private usernameKey: string = "user";
  private userImageKey: string = "userImage"

  private _authStatus = new Subject<boolean>();
  public authStatus = this._authStatus.asObservable();

  constructor(
    protected http: HttpClient) {
  }

  isAuthenticated(): boolean {
    return this.getToken() !== null;
  }

  isUser(): boolean {
    let splitToken: string[] = this.getRoles()
    return this.isAuthenticated() && splitToken.includes(Roles.User.toString())
  }

  isAdmin(): boolean {
    let splitToken: string[] = this.getRoles()
    return this.isAuthenticated() && splitToken.includes(Roles.Admin.toString())
  }

  getRoles() {
    if (this.isAuthenticated()) {
      let token = this.getToken();
      if (token) {
        let decodedJWT = JSON.parse(window.atob(token.split('.')[1]));
        return decodedJWT["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
      }
    }
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  getUsername(): string | null {
    return localStorage.getItem(this.usernameKey);
  }

  init(): void {
    if (this.isAuthenticated())
      this.setAuthStatus(true);
  }

  login(item: LoginRequest): Observable<LoginResult> {
    let url = environment.baseUrl + "api/Account/Login";
    return this.http.post<LoginResult>(url, item)
      .pipe(tap(loginResult => {
        if (loginResult.success && loginResult.token && loginResult.userName) {
          localStorage.setItem(this.tokenKey, loginResult.token);
          localStorage.setItem(this.usernameKey, loginResult.userName);
          this.setAuthStatus(true);
        }
      }));
  }

  register(item: RegisterRequest, file?: File): Observable<APIResult> {
    let url = environment.baseUrl + "api/Account/Register";
    let formData: FormData = new FormData();
    if (file)
      formData.append("profileImage", file, "profileImage")
    Object.entries(item).forEach(([key, value]) => {
      if (key != "dateOfBirth" && key != "observedCityIDs") formData.append(key, value);
    });
    formData.append("dateOfBirth", formatDate(item.dateOfBirth, "yyyy-MM-dd", "en"));
    if (item.observedCityIDs)
      for (let i = 0; i < item.observedCityIDs?.length; i++) {
        formData.append("observedCityIDs", item.observedCityIDs[i]);
      }


    const headers = new HttpHeaders({
      //'Content-Type': 'multipart/form-data',
      'Accept': 'application/json'
    });


    let options = { headers: headers };
    return this.http.post<APIResult>(url, formData, options);
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.usernameKey);
    this.setAuthStatus(false);
  }

  private setAuthStatus(isAuthenticated: boolean): void {
    this._authStatus.next(isAuthenticated);
  }
}
