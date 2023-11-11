import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { catchError, map, Observable, of } from 'rxjs';
import { AnimalStatus, AnimalType, Sex } from '../../shared/enums/enums';
import { AuthService } from '../../user/common/auth.service';
import { AnimalFilter } from '../common/animal-filter';
import { AnimalPreview } from '../common/animal-preview';
import { AnimalService } from '../common/animal.service';

@Component({
  selector: 'app-animal-list',
  templateUrl: './animal-list.component.html',
  styleUrls: ['./animal-list.component.scss']
})
export class AnimalListComponent implements OnInit {
  defaultPageIndex: number = 0;
  defaultPageSize: number = 10;
  @ViewChild(MatPaginator, {static: true}) paginator!: MatPaginator;
  animals$?: Observable<AnimalPreview[]>
  isBusy: boolean = false;

  form!: FormGroup;
  filters = <AnimalFilter>{};
  animalStatus = AnimalStatus;
  animalType = AnimalType;
  animalSex = Sex;

  constructor(
    protected authService: AuthService,
    protected animalservice: AnimalService) { }

  ngOnInit() {
    this.paginator._intl.itemsPerPageLabel = 'Megjelenített állatok száma:';
    this.paginator._intl.getRangeLabel = (page: number, pageSize: number, length: number) => {
      const start = page * pageSize + 1;
      const end = (page + 1) * pageSize;
      return `${start} - ${end} / ${length}`;
    };

    this.loadData();

    

    this.form = new FormGroup({
      name: new FormControl(''),
      status: new FormControl(''),
      city: new FormControl(''),
      type: new FormControl(''),
      sex: new FormControl('')
    });
  }
  loadData() {
    this.isBusy = true;
    const pageEvent = new PageEvent();
    pageEvent.pageIndex = this.defaultPageIndex;
    pageEvent.pageSize = this.defaultPageSize;
    this.getData(pageEvent);
  }
  getData(event: PageEvent) {
    this.animals$ = this.animalservice.getAnimals(event.pageIndex, event.pageSize, this.filters).pipe(map((result) => {
      this.paginator.length = result.totalCount;
      this.paginator.pageIndex = result.pageIndex;
      this.paginator.pageSize = result.pageSize;
      this.isBusy = false;
      return result.data;
    }),
      catchError((err: HttpErrorResponse) => {
        console.log(err.message);
        return of();
      })
    )
  }

  onSubmit() {
    this.filters.name = this.form.controls['name'].value;
    this.filters.status = this.form.controls['status'].value;
    this.filters.city = this.form.controls['city'].value;
    this.filters.type = this.form.controls['type'].value;
    this.filters.sex = this.form.controls['sex'].value;

    this.loadData();
  }
}
