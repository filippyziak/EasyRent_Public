import { RentalAdService } from '../services/rental-ad.service';
import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AlertifyService } from '../services/alertify.service';
import { RentalAdDetails } from '../models/RentalAdDetails';

@Injectable()
export class RentalAdResolver implements Resolve<RentalAdDetails> {
  constructor(
    private rentalAdService: RentalAdService,
    private router: Router,
    private alertify: AlertifyService
  ) {}

  public resolve(route: ActivatedRouteSnapshot): Observable<RentalAdDetails> {
    return this.rentalAdService.getRentalAd(route.params.rentalAdId).pipe(
      catchError((error) => {
        this.alertify.error('Problem retriving data');
        this.router.navigate(['']);
        return of(null);
      })
    );
  }
}
