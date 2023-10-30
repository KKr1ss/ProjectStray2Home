import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { AuthService } from "./auth.service";
import { Observable } from "rxjs";
import { Roles } from "../../shared/enums/enums";

@Injectable({
  providedIn: 'root'
})

export class AuthGuard {
  constructor(
    private authService: AuthService,
    private router: Router
  ) {}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):
    Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const requiresLogout = route.data['requiresLogout'] || false;
    const role = route.data['role'] || false

    if (requiresLogout) {
      if (!this.authService.isAuthenticated())
        return true;
      else
        this.router.navigate(['/user']);
        return false;
    }

    if (role && this.authService.isAuthenticated()) {
      if (role == Roles.User.toString()) {
        if (this.authService.isUser())
          return true;
        else {
          this.router.navigate(['/user']);
          return false;
        }
      }
      if (role == Roles.Admin.toString()) {
        if (this.authService.isAdmin())
          return true;
        else {
          this.router.navigate(['/user']);
          return false;
        }  
      }
    }

    if (this.authService.isAuthenticated())
      return true;

    this.router.navigate(['/user/login'], { queryParams: { returnUrl: state.url } });
    return false;
  }
}
