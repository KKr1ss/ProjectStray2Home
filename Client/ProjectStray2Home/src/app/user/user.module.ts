import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { userRoutes } from "./user.routes"
import { ProfileComponent } from "./profile/profile.component";
import { LoginComponent } from "./login/login.component"
import { RegisterComponent } from "./register/register.component";
import { AccountComponent } from './account/account.component';
import { SharedModule } from "../shared.module";

@NgModule({
  imports: [
    SharedModule,
    RouterModule.forChild(userRoutes)
  ],
  declarations: [
    ProfileComponent,
    LoginComponent,
    RegisterComponent,
    AccountComponent
  ],
  providers: [

  ]
})
export class UserModule { }
