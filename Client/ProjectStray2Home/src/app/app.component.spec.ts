import { Component } from '@angular/core';
import { TestBed } from '@angular/core/testing';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { AuthService } from './user/common/auth.service';

describe('AppComponent', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [
        AppComponent,
        MockNavMenuComponent,
        MockAppRoutingModule
      ]
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
 });

//  it(`should have as title 'ProjectStray2Home'`, () => {
//    const fixture = TestBed.createComponent(AppComponent);
//    const app = fixture.componentInstance;
//    expect(app.title).toEqual('ProjectStray2Home');
//  });

//  it('should render title', () => {
//    const fixture = TestBed.createComponent(AppComponent);
//    fixture.detectChanges();
//    const compiled = fixture.nativeElement as HTMLElement;
//    expect(compiled.querySelector('.content span')?.textContent).toContain('ProjectStray2Home app is running!');
//  });
});

@Component({
  selector: 'app-nav-menu',
  template: ''
})
class MockNavMenuComponent {
}

@Component({
  selector: 'router-outlet',
  template: ''
})
class MockAppRoutingModule {
}
