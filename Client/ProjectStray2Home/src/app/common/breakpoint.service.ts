import { BreakpointObserver } from "@angular/cdk/layout";
import { Injectable } from "@angular/core";
import { map, Observable } from "rxjs";

@Injectable({
  providedIn: 'root',
})
export class BreakpointService  {

  constructor(private breakpointObserver: BreakpointObserver) { }

  isTablet(): Observable<boolean> {
    return this.breakpointObserver.observe(['(min-width: 768px)']).pipe(map(({ matches }) => matches));
  }

  isDesktop(): Observable<boolean> {
    return this.breakpointObserver.observe(['(min-width: 1200px)']).pipe(map(({ matches }) => matches));
  }

}
