import { Component } from '@angular/core';
import { ContactUsModalComponent } from './shared/contact-us-modal.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SkillDto } from './data/skillDto';
import { CommonService } from './shared/common.service';

@Component({
  selector: 'gwc-root',
  templateUrl: './app.component.html'

})
export class AppComponent {
  title = 'Gray Wolf Corporation';

  public skills: SkillDto[] = [
    { section: 'Languages', description: 'AngularJs', years: 4, order: 1 },
    { section: 'Languages', description: 'Angular', years: 1, order: 1 },
    { section: 'Languages', description: 'C#', years: 10, order: 1 },
    { section: 'Languages', description: 'JavaScript', years: 10, order: 1 },
    { section: 'Web', description: 'Web API', years: 5, order: 2 },
    { section: 'Web', description: 'ASP MVC', years: 5, order: 2 },
    { section: 'Web', description: 'ASP .Net Core', years: 1, order: 2 },
    { section: 'Database', description: 'Sql Server', years: 10 , order: 3},
    { section: 'Database', description: 'EF', years: 8 , order: 3},
    { section: 'Database', description: 'EF Core', years: 1 , order: 3},
    { section: 'Azure', description: 'Azure Sql', years: 3 , order: 4},
    { section: 'Azure', description: 'Function Apps', years: 3 , order: 4},
    { section: 'Azure', description: 'Logic Apps', years: 2 , order: 4},
    { section: 'Azure', description: 'App Services', years: 3 , order: 4},
    { section: 'Azure', description: 'Azure Batch', years: 1 , order: 5},
    { section: 'APIs', description: 'SendGrid', years: 4 , order: 5},
    { section: 'APIs', description: 'SurveyMonkey', years: 2 , order: 5},
    { section: 'APIs', description: 'Contentful', years: .5 , order: 5},
    { section: 'APIs', description: 'GuruFocus', years: .5 , order: 5},
    { section: 'Other', description: 'NUnit', years: 4 , order: 6},
    { section: 'Other', description: 'AutoFac', years: 5 , order: 6},
    { section: 'Other', description: 'Docker', years: 1 , order: 6},
    { section: 'Other', description: 'Postman', years: 3 , order: 6}
  ]

  public categories: string[];

  constructor(private modalService: NgbModal, private commonService:CommonService) {
      this.categories =  Array.from(
        new Set(this.skills.sort(this.commonService.dynamicSort("order"))
        .map( y => y.section)));
   }

   getSkills(category:string){
      var test = this.skills
                  .filter(x => x.section == category)
                  .sort(this.commonService.dynamicSort("-years"));
      return test;
   }
  
  showContactForm(defaultIssue: string):void{
    const modalRef = this.modalService.open(ContactUsModalComponent);
    modalRef.componentInstance.defaultIssue = defaultIssue;
    modalRef.result.then();
  }
}
