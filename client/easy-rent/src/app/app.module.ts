import { RentalAdFormComponent } from './components/_modules/rentalAd/rentalAd-Form/rentalAd-Form.component';
import { RentalAdResolver } from './resolves/rental-ad.resolver';
import { RentalAdsResolver } from './resolves/rental-ads.resolver';
import { RegisterComponent } from './components/_modules/identity/register/register.component';
import { appRoutes } from './routes';
import { LoginComponent } from './components/_modules/identity/login/login.component';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { RentalAdCardComponent } from './components/_modules/rentalAd/rentalAd-card/rentalAd-card.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import {MatCardModule} from '@angular/material/card';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { GalleryModule } from 'ng-gallery';
import { RentalAdBrowserComponent } from './components/_modules/rentalAd/rentalAd-browser/rentalAd-browser.component';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { ReservationTenantListComponent } from './components/_modules/reservation/reservation-tenant-list/reservation-tenant-list.component';
import { ReservationResolver } from './resolves/reservation.resolver';
import { EditProfileComponent } from './components/_modules/identity/edit-profile/edit-profile.component';
import { AccountsResolver } from './resolves/acounts.resolver';
import { UserBrowserComponent } from './components/_modules/management/user-browser/user-browser.component';
import { RentalAdEditComponent } from './components/_modules/rentalAd/rentalAd-edit/rentalAd-edit.component';
import { RentalAdsOwnerListComponent } from './components/_modules/rentalAd/rentalAds-owner-list/rentalAds-owner-list.component';
import { RentalAdsOwnerResolver } from './resolves/rental-ads-owner.resolver';
import { RentalAdReservationsComponent } from './components/_modules/rentalAd/rentalAd-reservations/rentalAd-reservations.component';

export const tokenGetter = () => localStorage.getItem('token');

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    RentalAdCardComponent,
    NavbarComponent,
    RentalAdBrowserComponent,
    RentalAdFormComponent,
    ReservationTenantListComponent,
    EditProfileComponent,
    UserBrowserComponent,
    RentalAdEditComponent,
    RentalAdsOwnerListComponent,
    RentalAdReservationsComponent,
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    RouterModule.forRoot(appRoutes),
    BrowserAnimationsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCardModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatInputModule,
    GalleryModule,
    FontAwesomeModule,

    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: ['localhost:5000', 'host.docker.internal:5000']
      }
    })
  ],
  providers: [
    RentalAdsResolver,
    RentalAdResolver,
    AccountsResolver,
    DatePipe,
    ReservationResolver,
    RentalAdsOwnerResolver,
    CurrencyPipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
