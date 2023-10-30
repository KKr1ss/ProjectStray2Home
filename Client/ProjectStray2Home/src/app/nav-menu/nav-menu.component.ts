import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { AuthService } from '../user/common/auth.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnInit, OnDestroy {
  private destroySubject = new Subject();
  isLoggedIn: boolean = false;
  username: string | null = "";
  constructor(private authService: AuthService,
    private router: Router) {
    this.authService.authStatus
      .pipe(takeUntil(this.destroySubject))
      .subscribe(result => {
        this.isLoggedIn = result;
        if (result)
        {
          this.username = this.authService.getUsername()
        }
      })
  }
  onLogout(): void {
    this.authService.logout();
    this.router.navigate(["/"]);
  }
  ngOnInit(): void {
    if (this.authService.isAuthenticated())
    {
      this.username = this.authService.getUsername();
      this.isLoggedIn = true;
    }
    //this.isLoggedIn = this.authService.isAuthenticated();
    //if (this.isLoggedIn) {
    //  this.username = this.authService.getUsername();
  }
  ngOnDestroy() {
    this.destroySubject.next(true);
    this.destroySubject.complete();
  }
}
