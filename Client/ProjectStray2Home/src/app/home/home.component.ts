import { Component, OnInit } from '@angular/core';
import { delay, map, Observable, retryWhen} from 'rxjs';
import { AnimalService } from '../animal/common/animal.service';
import { AnimalPreview } from '../animal/common/models/animal-preview';
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
    }),
      retryWhen(error => error.pipe(delay(1000))
    ));
    this.isTablet$ = this.breakpointService.isTablet();
    this.isDesktop$ = this.breakpointService.isDesktop();
  }
}
