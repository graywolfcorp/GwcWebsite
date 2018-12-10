import { Component, OnInit, ViewChild } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { AgGridNg2 } from 'ag-grid-angular';
import { NgbPagination } from '@ng-bootstrap/ng-bootstrap';
import { TrService } from './tr.service';
import { map, catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { SourceDto } from '../data/sourceDto';

@Component({
  selector: 'gwc',
  templateUrl: './sources.component.html'  
})
export class SourcesComponent implements OnInit {
  @ViewChild('agGrid') agGrid: AgGridNg2;
  @ViewChild('ngbPagination') ngbPagination: NgbPagination;
  
  rowData: any;
  public paginationPageSize;
  public paginationNumberFormatter;
  public currentPage: number;
  public collectionSize: number;
  public pageInfo: string;
  private loading: boolean = false;
  private results: SourceDto[];

  columnDefs = [
    {headerName: 'Author', field: 'author', checkboxSelection: true },
    {headerName: 'Title', field: 'title' },
    {headerName: 'Publisher', field: 'publisher'},
    {headerName: 'Date', field: 'date'}
  ];

  constructor(private http: HttpClient, private trService:TrService ) { 
    this.currentPage = 1;
    this.paginationPageSize = 10;
  }

  ngOnInit() {
    this.trService.getSources().subscribe( data => {
      this.loading = false;
      this.collectionSize = data.length;
      this.rowData = data;
    });;       
  }

  getPageInfo(){
    let pageCalc = (this.currentPage * this.paginationPageSize);
    let pageTo = pageCalc > this.collectionSize ? this.collectionSize : pageCalc;
    let message = (pageCalc - (this.paginationPageSize - 1))  + ' to ' + pageTo + ' of ' + this.collectionSize;
    return message;
  }

  getSelectedRows() {
    const selectedNodes = this.agGrid.api.getSelectedNodes();
    const selectedData = selectedNodes.map( node => node.data );
    const selectedDataStringPresentation = selectedData.map( node => node.author + ' ' + node.title).join(', ');
    alert(`Selected nodes: ${selectedDataStringPresentation}`);
  }

  onPageChange(event){
    this.agGrid.api.paginationGoToPage(event-1);
    this.currentPage = event;    
  }

  private handleError(error: HttpErrorResponse): Observable<any> {        
    console.error(error);
    return throwError(error.error || 'Server error');
  }

}
