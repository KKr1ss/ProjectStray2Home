import { Pipe, PipeTransform } from '@angular/core';
@Pipe({ name: 'daysSinceStatusChange' })
export class DaysSinceStatusChangePipe implements PipeTransform {
    transform(statusDate: Date): number {
      const currentDate: Date = new Date()
      return Math.floor((currentDate.getTime() - statusDate.getTime()) / (1000 * 60 * 60 * 24))
    }
}
