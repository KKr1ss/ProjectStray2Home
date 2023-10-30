import { Pipe, PipeTransform } from '@angular/core';
import { Sex } from '../enums/enums';
@Pipe({ name: 'userSexConverter' })
export class UserSexConvererPipe implements PipeTransform {
    transform(sex: string): string {
      switch (sex) {
        case Sex.Female: return "Nő";
        case Sex.Male: return "Férfi";
        default: return "Férfi"
      }
        
    }
}
