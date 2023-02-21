import { RentalAdsOwnerResolver } from './resolves/rental-ads-owner.resolver';
import { RentalAdsOwnerListComponent } from './components/_modules/rentalAd/rentalAds-owner-list/rentalAds-owner-list.component';
import { RentalAdEditComponent } from './components/_modules/rentalAd/rentalAd-edit/rentalAd-edit.component';
import { AuthorizedGuard } from './guards/authorized.guard';
import { PlaceOwnerGuard } from './guards/place-owner.guard';
import { TenantGuard } from './guards/tenant.guard';
import { ModeratorGuard } from './guards/moderator.guard';
import { AccountsResolver } from './resolves/acounts.resolver';
import { ReservationTenantListComponent } from './components/_modules/reservation/reservation-tenant-list/reservation-tenant-list.component';
import { RentalAdFormComponent } from './components/_modules/rentalAd/rentalAd-Form/rentalAd-Form.component';
import { RentalAdsResolver } from './resolves/rental-ads.resolver';
import { RegisterComponent } from './components/_modules/identity/register/register.component';
import { Routes } from '@angular/router';
import { LoginComponent } from './components/_modules/identity/login/login.component';
import { RentalAdCardComponent } from './components/_modules/rentalAd/rentalAd-card/rentalAd-card.component';
import { RentalAdBrowserComponent } from './components/_modules/rentalAd/rentalAd-browser/rentalAd-browser.component';
import { RentalAdResolver } from './resolves/rental-ad.resolver';
import { AnonymousGuard } from './guards/anonymous.guard';
import { ReservationResolver } from './resolves/reservation.resolver';
import { EditProfileComponent } from './components/_modules/identity/edit-profile/edit-profile.component';
import { UserBrowserComponent } from './components/_modules/management/user-browser/user-browser.component';

export const appRoutes: Routes = [
  { path: 'login', component: LoginComponent, canActivate: [AnonymousGuard]},
  { path: 'register', component: RegisterComponent, canActivate: [AnonymousGuard]},
  
  { path: 'profile', component: EditProfileComponent, canActivate: [AuthorizedGuard]},
  
  { path: 'rentalAd/form', component: RentalAdFormComponent, canActivate:[PlaceOwnerGuard]},
  { path: 'owner', component: RentalAdsOwnerListComponent, resolve:{rentalAdsResolver: RentalAdsOwnerResolver} ,canActivate:[PlaceOwnerGuard]},

  { path: 'rentalAd/:rentalAdId', component: RentalAdCardComponent, resolve: {rentalAdResolver: RentalAdResolver, reservationsResolver: ReservationResolver}},
  { path: 'rentalAd/edit/:rentalAdId', component: RentalAdEditComponent, resolve: {rentalAdResolver: RentalAdResolver}},
  { path: 'browser', component: RentalAdBrowserComponent, resolve: {rentalAdsResolver: RentalAdsResolver}},
  
  { path: 'tenant', component: ReservationTenantListComponent, canActivate:[TenantGuard]},
  { path: 'management/profiles', component: UserBrowserComponent, canActivate:[ModeratorGuard], resolve: {accountResolver: AccountsResolver}},
  
  { path: '**', redirectTo: 'browser', pathMatch: 'full' },
    
  ];
  