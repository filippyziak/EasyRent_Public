import { AlertifyService } from './alertify.service';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { DatePipe } from '@angular/common';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ReservationService {
  constructor(
    private http: HttpClient,
    private datePipe: DatePipe,
    private route: Router,
    private alertify: AlertifyService
  ) {}

  public reserve(
    rentalAdId: string,
    ownerId: string,
    arrivalDate: string,
    departureDate: string
  ) {
    console.log(ownerId)
    return this.http
      .post(environment.apiUrl + 'reservation/placereservation', {
        rentalAdId: rentalAdId,
        OwnerId: ownerId,
        arrivalDate: this.datePipe.transform(arrivalDate) ?? '',
        departureDate: this.datePipe.transform(departureDate) ?? '',
      })
      .subscribe((res) => {
        this.alertify.success("Reservation added successfully");
        this.route.navigate(['/tenant']);
      }, error => {
        this.alertify.error("Cannot create reservation");
      });
  }

  public reservationsForTenant(): Observable<any> {
    return this.http.get<any>(
      environment.apiUrl + 'reservation/placereservation/tenant'
    );
  }

  public getReservationsForRentalAd(rentalAdId: string): Observable<any> {
    return this.http.get<any>(
      environment.apiUrl + 'reservation/placereservation/rentalAd',
      {
        params: new HttpParams()
        .set("RentalAdId", rentalAdId)
      }
    );
  }

  public review(
    description: string,
    reviewScore: number,
    reservationId: string,
    rentalAdId: string
  ) {
    return this.http
      .post(environment.apiUrl + 'reservation/placereservation/review', {
        ReviewDescription: description,
        ReviewScore: reviewScore,
        PlaceReservationId: reservationId,
      })
      .subscribe((res) => {
        this.alertify.success("Review added successfully");
      }, error => {
        this.alertify.error("Cannot add review");
      });
  }

  public cancelReservation(reservationId: string){
    this.http
      .delete(environment.apiUrl + 'reservation/placereservation/cancel', {
        params: new HttpParams()
          .set('PlaceReservationId', reservationId),
      })
      .toPromise()
      .then(
        (res) => {
          this.alertify.success('Reservation removed');
        },
        (error) => {
          this.alertify.error('Removing reservation failed');
        }
      );
  }
}
