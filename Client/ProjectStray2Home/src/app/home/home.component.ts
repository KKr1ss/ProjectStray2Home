import { Component, OnInit } from '@angular/core';
import { map, Observable } from 'rxjs';
import { AnimalPreview } from '../animal/common/animal-preview';
import { AnimalService } from '../animal/common/animal.service';
import { BreakpointService } from '../common/breakpoint.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  animals$?: Observable<AnimalPreview[]>
  isTablet$?: Observable<boolean>
  isDesktop$?: Observable<boolean>

  constructor(protected animalservice: AnimalService,
    private breakpointService: BreakpointService) { }

  ngOnInit(): void {
    this.animals$ = this.animalservice.getAnimals(0, 4).pipe(map((result) => {
      return result.data;
    }));
    this.isTablet$ = this.breakpointService.isTablet();
    this.isDesktop$ = this.breakpointService.isDesktop();
  }
}
