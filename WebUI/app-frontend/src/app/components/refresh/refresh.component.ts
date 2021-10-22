import { AfterViewInit, Component } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-refresh',
  templateUrl: './refresh.component.html'
})
export class RefreshComponent implements AfterViewInit {

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private location: Location) 
    { }


  ngAfterViewInit() {

    this.activatedRoute.queryParams.subscribe((routeParams: Params) => {
      setTimeout(() => {
          this.location.back();
      }, 50);
    });

}

}
