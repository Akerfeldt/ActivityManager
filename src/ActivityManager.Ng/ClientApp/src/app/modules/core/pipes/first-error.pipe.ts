import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'firstError' })
export class FirstErrorPipe implements PipeTransform {
  transform(value: any): string {
    if (!value)
      return null;

    var keys = Object.keys(value);
    if (keys && keys.length > 0) {
      return keys[0];
    }
    return null;
  }
}
