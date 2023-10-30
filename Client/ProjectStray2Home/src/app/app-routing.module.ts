import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full'},
  { path: 'user',
    loadChildren: () => import('./user/user.module').then(x => x.UserModule)
  },
  {
    path: 'animals',
    loadChildren: () => import('./animal/animal.module').then(x => x.AnimalModule)
  }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
