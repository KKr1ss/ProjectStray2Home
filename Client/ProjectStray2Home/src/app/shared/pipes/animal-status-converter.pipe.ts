import { Pipe, PipeTransform } from '@angular/core';
import { AnimalStatus } from '../enums/enums';
@Pipe({ name: 'animalStatusConverter' })
export class AnimalStatusConvererPipe implements PipeTransform {
    transform(status: string): string {
      switch (status) {
        case AnimalStatus.Lost: return "Elveszett";
        case AnimalStatus.Rescued: return "Befogadva";
        case AnimalStatus.Stray: return "Kóbor";
        case AnimalStatus.Home: return "Hazatért";
        default: return "hazatért"
      }
        
    }
}
