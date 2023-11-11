import { Pipe, PipeTransform } from '@angular/core';
import { AnimalType } from '../enums/enums';
@Pipe({ name: 'animalTypeConverter' })
export class AnimalTypeConvererPipe implements PipeTransform {
    transform(type: string): string {
      switch (type) {
        case AnimalType.Dog: return "Kutya";
        case AnimalType.Cat: return "Macska";
        case AnimalType.Other: return "Egyéb";
        default: return "Macska"
      }
    }
}
