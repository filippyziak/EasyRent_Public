import { IdentityService } from './../services/identity.service';
import { Injectable } from "@angular/core";
import {Resolve, Router, ActivatedRouteSnapshot} from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AlertifyService } from '../services/alertify.service';

@Injectable()
export class AccountsResolver implements Resolve<any>{
    constructor(private identityService: IdentityService, private router: Router, private alertify: AlertifyService) {}

    public resolve(route: ActivatedRouteSnapshot): Observable<any> {
        return this.identityService.getAccounts().pipe(
            catchError(error =>{
                this.alertify.error('Problem retriving data');
                this.router.navigate(['']);
                return of(null);
            })
        );
    }
}