import { Pipe, PipeTransform } from '@angular/core';
@Pipe({ name: 'count' })
export class Count implements PipeTransform {
    transform(array, filterField, compareValue) {
        try {
            let obj = array.filter(x => x[filterField] === compareValue);
            return obj.length;
        } catch (ex) {
        }
        return 0;
    }
}