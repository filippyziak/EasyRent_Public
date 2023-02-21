import { RentalAdService } from '../services/rental-ad.service';
import { Injectable } from "@angular/core";
import {Resolve, Router, ActivatedRouteSnapshot} from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AlertifyService } from '../services/alertify.service';
import { RentalAdCard } from '../models/RentalAdCard';

@Injectable()
export class RentalAdsOwnerResolver implements Resolve<RentalAdCard[] | null>{
    constructor(private rentalAdService: RentalAdService, private router: Router, private alertify: AlertifyService) {}

    public resolve(route: ActivatedRouteSnapshot): Observable<RentalAdCard[] | null> {
        return this.rentalAdService.getRentalAdForPlaceOwner().pipe(
            catchError(error =>{
                this.alertify.error('Problem retriving data');
                this.router.navigate(['']);
                return of(null);
            })
        );
    }
}