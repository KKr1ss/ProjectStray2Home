import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './home/home.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { AppRoutingModule } from './app-routing.module';
import { AuthInterceptor } from './user/common/auth.interceptor';
import { AnimalModule } from './animal/animal.module';
import { FooterComponent } from './footer/footer.component';
import { SharedModule } from './shared.module';
import { SmallAnimalThumbnailComponent } from './animal/thumbnails/small-animal-thumbnail/small-animal-thumbnail.component';

@NgModule({
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AnimalModule,
    SharedModule
  ],
  declarations: [
    AppComponent,
    HomeComponent,
    NavMenuComponent,
    FooterComponent
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  exports: [SmallAnimalThumbnailComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
