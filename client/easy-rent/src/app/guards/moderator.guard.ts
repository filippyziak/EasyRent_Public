import { AlertifyService } from './../services/alertify.service';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { IdentityService } from '../services/identity.service';

@Injectable({
    providedIn: 'root'
})
export class ModeratorGuard implements CanActivate {
    constructor(private identittyService: IdentityService, private router: Router, private alertify: AlertifyService) { }

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

        if (this.identittyService.isModerator()) {
            return true;
        }

        this.router.navigate(['/browser']);
        this.alertify.message('You are not allowed to view this page');

        return false;
    }
}