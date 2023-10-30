import { HomeComponent } from '../home/home.component';
import { AccountComponent } from './account/account.component';
import { AuthGuard } from './common/auth.guard';
import { LoginComponent } from './login/login.component';
import { ProfileComponent } from './profile/profile.component';
import { RegisterComponent } from './register/register.component';

type PathMatch = "full" | "prefix" | undefined;

export const userRoutes = [
  {
    path: 'login', component: LoginComponent,
    data: { requiresLogout: true }, canActivate: [AuthGuard]
  },
  {
    path: 'register', component: RegisterComponent,
    data: { requiresLogout: true }, canActivate: [AuthGuard]
  },
  { path: 'myaccount', component: AccountComponent, canActivate: [AuthGuard] },
  { path: ':username', component: ProfileComponent },
  { path: '', redirectTo: 'myaccount', pathMatch: 'full' as PathMatch }
]
