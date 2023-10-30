import { Pipe, PipeTransform } from '@angular/core';
import { City } from '../../common/city';
@Pipe({ name: 'cityFilter' })
export class CityFilterPipe implements PipeTransform {
  transform(filterValue: string ,allCities: City[]): City[] {
    const filteredCities = allCities?.filter(
      (city: City) => city.name.toLowerCase().startsWith(filterValue))
    return filteredCities?.slice(0, 5);
  }
}
