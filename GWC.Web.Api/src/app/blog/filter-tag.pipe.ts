import { Pipe, PipeTransform } from '@angular/core';
import { TagDto } from '../data/tagDto';
import { ContentfulTagDto } from '../data/contentful-tag-dto';
@Pipe({
  name: 'filterTag'
})
export class FilterTagPipe implements PipeTransform {
  transform(blogTags: ContentfulTagDto[], allTags: TagDto[]): TagDto[] {
    if(!blogTags) return [];

    return allTags.filter( x => {
        return blogTags.map(function(a) {return a.sys.id}).includes(x.id);
    })
   }
}