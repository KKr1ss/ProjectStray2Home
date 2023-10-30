import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { map, Observable } from 'rxjs';
import { AuthService } from '../../user/common/auth.service';
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
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  animals$?: Observable<AnimalPreview[]>
  isBusy: boolean = false;

  constructor(
    protected authService: AuthService,
    protected animalservice: AnimalService ) { }

  ngOnInit() {
    this.isBusy = true;
    this.loadData();
  }
  loadData() {
    const pageEvent = new PageEvent();
    pageEvent.pageIndex = this.defaultPageIndex;
    pageEvent.pageSize = this.defaultPageSize;
    this.getData(pageEvent);
  }
  getData(event: PageEvent) {
    this.animals$ = this.animalservice.getAnimals(event.pageIndex, event.pageSize).pipe(map((result) => {
      this.paginator.length = result.totalCount;
      this.paginator.pageIndex = result.pageIndex;
      this.paginator.pageSize = result.pageSize;
      this.isBusy = false;
      return result.data;
    })
    )
  }
}
