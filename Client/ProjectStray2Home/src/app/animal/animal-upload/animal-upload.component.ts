import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { map, Observable, startWith, tap } from 'rxjs';
import { APIResult } from '../../common/api-result';
import { City } from '../../common/city';
import { CityService } from '../../common/city.service';
import { AnimalStatus, AnimalType, Sex } from '../../shared/enums/enums';
import { AuthService } from '../../user/common/auth.service';
import { AnimalRequest } from '../common/animal-request';
import { AnimalService } from '../common/animal.service';

@Component({
  selector: 'app-animal-upload',
  templateUrl: './animal-upload.component.html',
  styleUrls: ['./animal-upload.component.scss']
})
export class AnimalUploadComponent implements OnInit {
  title?: string;
  cities$?: Observable<City[]>;
  uploadResult?: APIResult;
  form!: FormGroup;
  uploadFiles: File[] = [];
  fileName?: string;
  animalStatus: string[] = Object.values(AnimalStatus);
  sex: string[] = Object.values(Sex);
  animalTypes: string[] = Object.values(AnimalType);
  isBusy: boolean = false;

  allCities?: City[]
  filteredCities?: Observable<City[]>

  constructor(
    private router: Router,
    private cityService: CityService,
    private animalService: AnimalService,
    private authService: AuthService) { }

  ngOnInit() {
    this.loadCities()
    this.form = new FormGroup({
      type: new FormControl('', Validators.required),
      breed: new FormControl('', Validators.required),
      name: new FormControl('', Validators.required),
      sex: new FormControl('', Validators.required),
      dateOfBirth: new FormControl(),
      characteristic: new FormControl('', Validators.required),
      behavior: new FormControl('', Validators.required),
      isChipped: new FormControl(Validators.required),
      status: new FormControl('', Validators.required),
      statusDescription: new FormControl(''),
      city: new FormControl('', Validators.required),
      imageFiles: new FormControl(this.fileName, Validators.required),

      cityInput: new FormControl('')
    });

    this.filteredCities = this.form.controls['cityInput'].valueChanges.pipe(
      startWith(''),
      map(value => {
        const name = typeof value === 'string' ? value : value?.name;
        return name ? this._filter(name as string) : this.allCities!.slice(0, 10);
      }),
    );
  }

  loadCities() {
    this.cities$ = this.cityService.getCities().pipe(tap(x => { this.allCities = x }));
  }

  private _filter(name: string): City[] {
    const filterValue = name.toLowerCase();

    return this.allCities!.filter(option => option.name.toLowerCase().startsWith(filterValue));
  }

  displayFn(city: City): string {
    return city && city.name ? city.name : '';
  }

  onCurrentCitySelectChange(city?: City) {
    this.form.controls['city'].setValue(city)
  }

  onFileChange(event: any) {

    this.uploadFiles = []
    if (event.target.files.length > 1)
      this.fileName = `${event.target.files.length} db fájl kiválasztva`
    else
      this.fileName = event.target.files[0].name;
    for (let i = 0; i < event.target.files.length; i++) {
      this.uploadFiles.push(event.target.files[i]);
    }
    this.form.controls['imageFiles'].patchValue(true)
  }

  onSubmit() {
    this.isBusy = true;
    let animal = <AnimalRequest>{};
    animal.type = this.form.controls['type'].value;
    animal.breed = this.form.controls['breed'].value;
    animal.name = this.form.controls['name'].value;
    animal.sex = this.form.controls['sex'].value;
    animal.dateOfBirth = this.form.controls['dateOfBirth'].value;
    animal.characteristic = this.form.controls['characteristic'].value;
    animal.behavior = this.form.controls['behavior'].value;
    animal.isChipped = this.form.controls['isChipped'].value;
    animal.status = this.form.controls['status'].value;
    animal.statusDescription = this.form.controls['statusDescription'].value;
    animal.cityID = this.form.controls['city'].value.id;
    animal.userName = this.authService.getUsername()!;

    //let formData = new FormData();
    //for (let i = 0; i < this.uploadFiles.length; i++) {
    //  formData.append('files', this.uploadFiles[i]);
    //}

    this.animalService
      .upload(animal, this.uploadFiles)
      .subscribe(result => {
        console.log(result);
        this.uploadResult = result;
        if (result.success) {
          this.router.navigate(["/animals/"]);
        }
        this.isBusy = false;
      }, error => {
        console.log(error);
        if (error.status == 401) {
          this.uploadResult = error.error;
        }
        else {
          this.uploadResult = {
            success: false,
            message: "Ismeretlen hiba történt"
          }
        }
        this.isBusy = false;
      });
  }

  getErrors(
    control: AbstractControl,
    displayName: string,
    customMessages: { [key: string]: string } | null = null
  ): string[] {
    let errors: string[] = [];
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
