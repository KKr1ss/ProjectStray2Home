/* eslint-disable prefer-const */
import { Component, OnInit} from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';
import { AuthService } from '../common/auth.service';
import { APIResult } from '../../common/api-result';
import { RegisterRequest } from '../common/models/register-request';
import { map, Observable, startWith, tap } from 'rxjs';
import { City } from '../../common/city';
import { CityService } from '../../common/city.service';

export interface User {
  name: string;
}

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerResult?: APIResult;
  cities$?: Observable<City[]>
  form!: FormGroup;
  uploadFile?: File;
  fileName?: string;
  isBusy: boolean = false;
  userImagePreview?: string;

  stepCounter = 0;
  stepArray = [true, false, false];

  allCities?: City[]
  filteredCities?: Observable<City[]>
  filteredObservedCity?: Observable<City[]>;
  selectedCities: City[] = new Array<City>()

  constructor(
    private router: Router,
    private cityService: CityService,
    private authService: AuthService) { }

  ngOnInit() {
    this.loadCities()
    this.form = new FormGroup({
      personalForm: new FormGroup({
        userName: new FormControl('', Validators.required),
        email: new FormControl('', Validators.required),
        password: new FormControl('', Validators.required),
        firstname: new FormControl('', Validators.required),
        lastname: new FormControl('', Validators.required),
        sex: new FormControl('', Validators.required),
        dateOfBirth: new FormControl(new Date(), [Validators.required]),
        phoneNumber: new FormControl('', Validators.required),
        currentCity: new FormControl('', Validators.required),
        description: new FormControl(),

        currentCityInput: new FormControl('')
      }),
      citiesForm: new FormGroup({
        cityInput: new FormControl(''),
        city: new FormControl(Validators.required)
      })
    })

    this.filteredCities = this.getPersonalForm().controls['currentCityInput'].valueChanges.pipe(
      startWith(''),
      map(value => {
        const name = typeof value === 'string' ? value : value?.name;
        return name ? this._filter(name as string) : this.allCities!.slice(0, 10);
      }),
    );

    this.filteredObservedCity = this.getCitiesForm().controls['cityInput'].valueChanges.pipe(
      startWith(''),
      map(value => {
        const name = typeof value === 'string' ? value : value?.name;
        return name ? this._filter(name as string) : this.allCities!.slice(0, 10);
      }),
    );
  }

  loadCities() {
    this.cities$ = this.cityService.getCities().pipe(tap(x => {
      this.allCities = x;
    }));
  }

  private _filter(name: string): City[] {
    const filterValue = name.toLowerCase();

    return this.allCities!.filter(option => option.name.toLowerCase().startsWith(filterValue));
  }

  displayFn(city: City): string {
    return city && city.name ? city.name : '';
  }

  onCurrentCitySelectChange(city?: City) {
    this.getPersonalForm().controls['currentCity'].setValue(city)
  }

  onObservedCitySelectChange(city?: City) {
    this.getCitiesForm().controls['cityInput'].setValue('');
    if (!this.selectedCities.includes(city!)) {
      this.selectedCities.push(city!);
    }
  }

  removeCity(city: City) {
    const index: number = this.selectedCities.indexOf(city);
    if (index !== -1) {
      this.selectedCities.splice(index, 1)
    }
  }

  getPersonalForm(): FormGroup {
    return this.form.get('personalForm') as FormGroup
  }

  getCitiesForm(): FormGroup {
    return this.form.get('citiesForm') as FormGroup
  }

  onFileChange(event: any) {
    this.fileName = event.target.files[0].name;
    this.uploadFile = event.target.files[0];

    if (event.target.files && event.target.files[0]) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.userImagePreview = e.target.result;
      };
      reader.readAsDataURL(event.target.files[0]);
    }
  }

  onSubmit() {
    this.isBusy = true;
    let registerRequest = <RegisterRequest>{};
    registerRequest.userName = this.getPersonalForm().controls['userName'].value;
    registerRequest.email = this.getPersonalForm().controls['email'].value;
    registerRequest.password = this.getPersonalForm().controls['password'].value;
    registerRequest.firstName = this.getPersonalForm().controls['firstname'].value;
    registerRequest.lastName = this.getPersonalForm().controls['lastname'].value;
    registerRequest.sex = this.getPersonalForm().controls['sex'].value;
    registerRequest.dateOfBirth = this.getPersonalForm().controls['dateOfBirth'].value;
    registerRequest.phoneNumber = this.getPersonalForm().controls['phoneNumber'].value;
    registerRequest.currentCityID = this.getPersonalForm().controls['currentCity'].value.id;
    registerRequest.description = this.getPersonalForm().controls['description'].value;
    registerRequest.observedCityIDs = this.selectedCities.map(x => x.id.toString());

    //let formData = new FormData();
    //if (this.uploadFile) {
    //  formData.append('file', this.uploadFile);
    //}

    this.authService
      .register(registerRequest, this.uploadFile)
      .subscribe(result => {
        console.log(result);
        this.registerResult = result;
        if (result.success) {
          this.router.navigate(["/user/login"]);
        }
        this.isBusy = false;
      }, error => {
        console.log(error);
        if (error.status == 400) {
          this.registerResult = error.error;
        }
        this.isBusy = false;
      });
  }

  stepForward() {
    this.stepArray[this.stepCounter] = false;
    this.stepCounter++;
    if (this.stepCounter > this.stepArray.length - 1) {
      this.stepCounter = this.stepArray.length - 1;
    }

    this.stepArray[this.stepCounter] = true;
  }

  stepBackward() {
    this.stepArray[this.stepCounter] = false;
    this.stepCounter--;
    if (this.stepCounter < 0) {
      this.stepCounter = 0;
    }
    this.stepArray[this.stepCounter] = true;
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
