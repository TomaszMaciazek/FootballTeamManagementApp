import { Component, OnInit, Renderer2 } from '@angular/core';

@Component({
  selector: 'blank-layout',
  templateUrl: './blank-layout.component.html'
})
export class BlankLayoutComponent{

  constructor(private renderer : Renderer2){}

  ngAfterViewInit() {
    this.renderer.addClass(document.body, 'gray-bg');
  }

  ngOnDestroy() {
    this.renderer.removeClass(document.body, 'gray-bg');
  }

}
