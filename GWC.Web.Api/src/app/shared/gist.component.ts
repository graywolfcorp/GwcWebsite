import { Component, Input, ViewChild, ElementRef, AfterViewInit, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'gwc-gist',
  template:`
  <iframe #iframe type="text/javascript" width="100%" frameborder="0" style="height:0px;"></iframe>
  `,
  styleUrls: [],
  encapsulation: ViewEncapsulation.None
  
})
export class GistComponent implements AfterViewInit {
  @ViewChild('iframe') iframe:ElementRef;
  @Input() gistId: string;
  @Input() gistHeight: string;

  ngAfterViewInit() {
    if (this.gistId == null || this.gistId.length == 0) return;
    let fileName = this.gistId + '.js'; 
    this.iframe.nativeElement.id = 'gist-' + this.gistId;
    this.iframe.nativeElement.style = this.gistHeight ? 'height:' + this.gistHeight  : 'height:inherit;';
    let doc = this.iframe.nativeElement.contentDocument || this.iframe.nativeElement.contentElement.contentWindow;
      let content = `
        <html>
        <head>
          <base target="_parent">
        </head>
        <body onload="parent.document.getElementById('${this.iframe.nativeElement.id}')">
        <script type="text/javascript" src="https://gist.github.com/graywolfcorp/${fileName}"></script>
        </body>
      </html>
    `;
    doc.open();
    doc.write(content);
    doc.close();
  }
}