import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';
import { AuthService } from '../common/auth.service';
import { LoginRequest } from '../common/models/login-request';
import { LoginResult } from '../common/models/login-result';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  title?: string;
  loginResult?: LoginResult;
  form!: FormGroup;
  isBusy: boolean = false

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private authService: AuthService) { }

  ngOnInit() {
    this.form = new FormGroup({
      userName: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required)
    });
  }

  onSubmit() {
    this.isBusy = true;
    const loginRequest = <LoginRequest>{};
    loginRequest.userName = this.form.controls['userName'].value;
    loginRequest.password = this.form.controls['password'].value;

    this.authService
      .login(loginRequest)
      .subscribe(result => {
        console.log(result);
        this.loginResult = result;
        if (result.success) {
          const returnUrl = this.activatedRoute.snapshot.queryParamMap.get('returnUrl') || '/';
          this.router.navigateByUrl(returnUrl)
        }
        this.isBusy = false;
      }, error => {
        console.log(error);
        if (error.status == 401) {
          this.isBusy = false;
          this.loginResult = error.error;
        }
      });
  }

  getErrors(
    control: AbstractControl,
    displayName: string,
    customMessages: { [key: string]: string } | null = null
  ): string[] {
    const errors: string[] = [];
    Object.keys(control.errors || {}).forEach((key) => {
      switch (key) {
        case 'required':
          errors.push(`${displayName} ${customMessages?.[key] ?? " megadása szükséges."}`);
          break;
        case 'pattern':
          errors.push(`${displayName} ${customMessages?.[key] ?? " hibás karaktereket tartalmaz."}`);
          break;
        default:
          errors.push(`${displayName} nem valid.`);
          break;
      }
    });
    return errors;
  }
}
