import { PipeTransform, Pipe } from '@angular/core';

@Pipe({
    name: 'filterBy',
    pure: false
})
export class FilterByPipe implements PipeTransform {
    transform(items: any[], callback: (item: any) => boolean): any {
        if (!items || !callback) {
            return items;
        }
        return items.filter(item => callback(item));
    }
}