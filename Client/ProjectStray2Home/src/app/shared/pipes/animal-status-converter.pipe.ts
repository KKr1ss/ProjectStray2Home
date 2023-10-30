import { Pipe, PipeTransform } from '@angular/core';
import { AnimalStatus } from '../enums/enums';
@Pipe({ name: 'animalStatusConverter' })
export class AnimalStatusConvererPipe implements PipeTransform {
    transform(status: string): string {
      switch (status) {
        case AnimalStatus.Home: return "otthon";
        case AnimalStatus.Lost: return "elveszett";
        case AnimalStatus.Rescued: return "befogadva";
        case AnimalStatus.Stray: return "kóbor"; 
        default: return "otthon"
      }
        
    }
}
