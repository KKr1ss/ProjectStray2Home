import { Pipe, PipeTransform } from '@angular/core';
import { Sex } from '../enums/enums';
@Pipe({ name: 'animalSexConverter' })
export class AnimalSexConvererPipe implements PipeTransform {
    transform(sex: string): string {
      switch (sex) {
        case Sex.Female: return "Nőstény";
        case Sex.Male: return "Hím";
        default: return "Hím"
      }
        
    }
}
